using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("EnemyBase: Inspector Set General Fields")]
    public int health = 1;

    [Header("EnemyBase: Inspector Set General Firing Fields")]
    public GameObject bullet;
    public float timeBetweenBulletShots = 1.0f;

    [Header("EnemyBase: Inspector Set General Movement Fields")]
    public float newForceWeight = 0.1f;
    public float maxSpeed = 2.0f;

    [Header("EnemyBase: Inspector Set Wander Movement Fields")]
    public float timeBetweenForceShift = 1.0f;

    [Header("EnemyBase: Inspector Set Aggro Movement Fields")]
    public float aggroReflectTime = 1.0f;

    [Header("EnemyBase: Dynamically Set General Fields")]
    public GameObject poi;
    public Rigidbody rigid;
    public EnemySpawner enemySpawn;

    [Header("EnemyBase: Dynamically Set General Firing Fields")]
    public float elapsedFireTime;

    [Header("EnemyBase: Dynamically Set General Movement Fields")]
    public Vector3 currentForce;
    public float elapsedForceShiftTime;
    public Vector3? collisionForce; // The ? makes this nullable.

    // Initialize all needed starting values.
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        currentForce = Vector3.zero;

        // Ensure that the force "shifts" immediately,
        // i.e. give it an initial random force.
        elapsedForceShiftTime = timeBetweenForceShift;
        elapsedFireTime = timeBetweenBulletShots;
        collisionForce = null;

        return;
    }

    void Start()
    {
        poi = Hero.hero.gameObject;
    }

    // Update will keep shooting as fluid as possible
    // keeping within a set timer.
    // This is a non-virtual interface allowing easy fire control.
    void Update()
    {
        fire();
        return;
    }

    // FixedUpdate works best for movement,
    // so this non-virtual interface allows easy movement control.
    void FixedUpdate()
    {
        move();
        return;
    }

    // When a collision occurs, record it to be used
    // by the current movement function.
    void OnCollisionEnter(Collision collision)
    {
        Vector3 tempForce = Vector3.Reflect(
            currentForce, collision.contacts[0].normal);

        tempForce.Normalize();
        collisionForce = tempForce * maxSpeed;

        return;
    }

    // Deal with being hit by a bullet.
    // If this enemy dies, it acts as a non-virtual interface
    // to deal with on-death actions.
    void OnTriggerEnter(Collider other)
    {
        // We are likely to change self-damage rules for enemy bullets.
        BulletBase bullet = other.gameObject.GetComponent<BulletBase>();
        if ((bullet != null) && !bullet.ignoreEnemies)
        {
            health -= bullet.bulletDamage;
            if (health <= 0)
            {
                if (enemySpawn != null)
                {
                    enemySpawn.enemy = null;
                }
                onDeath();

                Destroy(gameObject);
            }
        }

        return;
    }

    // This virtual function fires this enemy's weapon.
    // The base behavior is nothing, it must be overriden
    // if an enemy is too shoot.
    protected virtual void fire()
    {
        return;
    }

    // This virtual function moves this enemy.
    // The base behavior is nothing, it must be overriden
    // if an enemy is to move.
    protected virtual void move()
    {
        return;
    }

    // This virtual function performs on-death actions.
    // The base behavior is nothing (destruction happens elsewhere).
    protected virtual void onDeath()
    {
        return;
    }

    // This function performs the wander movement action.
    // The enemy moves in a fluid manner in random directions.
    // This is assumed to only ever be called through FixedUpdate().
    protected void wanderMovement()
    {
        if (collisionForce != null)
        {
            currentForce = collisionForce.Value;
            collisionForce = null;
            elapsedForceShiftTime = 0f;

            rigid.velocity = (1 - newForceWeight) * rigid.velocity +
                newForceWeight * currentForce;

            return;
        }

        elapsedForceShiftTime += Time.fixedDeltaTime;

        if (elapsedForceShiftTime >= timeBetweenForceShift)
        {
            elapsedForceShiftTime -= timeBetweenForceShift;

            // Ensure that the force is not (0,0,0) with this do-while.
            do
            {
                // Assign the current force, subtracting 0.5f
                // in order to make it an even distribution + and -.
                currentForce.x = Random.value - 0.5f;
                currentForce.y = Random.value - 0.5f;
                currentForce.z = 0;
            } while (currentForce == Vector3.zero);

            currentForce.Normalize();
            currentForce *= maxSpeed;
        }

        rigid.velocity = (1 - newForceWeight) * rigid.velocity +
            newForceWeight * currentForce;
        return;
    }

    // This function performs the aggro movement action.
    // The enemy moves toward the poi.
    // This is assumed to only ever be called through FixedUpdate().
    protected void aggroMovement()
    {
        elapsedForceShiftTime += Time.fixedDeltaTime;

        if (elapsedForceShiftTime >= aggroReflectTime)
        {
            if (collisionForce != null)
            {
                currentForce = collisionForce.Value;
                collisionForce = null;
                elapsedForceShiftTime = 0f;

                rigid.velocity = (1 - newForceWeight) * rigid.velocity +
                    newForceWeight * currentForce;

                return;
            }

            elapsedForceShiftTime = aggroReflectTime;

            currentForce = poi.transform.position - transform.position;
            currentForce.Normalize();
            currentForce *= maxSpeed;
        }

        rigid.velocity = (1 - newForceWeight) * rigid.velocity +
            newForceWeight * currentForce;
        return;
    }

    // This function performs a standard direct shot at the poi.
    // The enemy shoots directly at the poi.
    // This is assumed to only ever be called through Update().
    protected void standardDirectShot()
    {
        if (elapsedFireTime < timeBetweenBulletShots)
        {
            return;
        }
        elapsedFireTime = 0f;

        GameObject goBullet = Instantiate(bullet);
        BulletBase bulletBase = goBullet.GetComponent<BulletBase>();

        goBullet.transform.position = transform.position;

        Vector3 vel = poi.transform.position - transform.position;
        vel.Normalize();
        vel *= bulletBase.bulletSpeed;
        bulletBase.rigid.velocity = vel;

        return;
    }

    protected void randomShot()
    {
        if (elapsedFireTime < timeBetweenBulletShots)
        {
            return;
        }
        elapsedFireTime = 0f;

        GameObject goBullet = Instantiate(bullet);
        BulletBase bulletBase = goBullet.GetComponent<BulletBase>();

        goBullet.transform.position = transform.position;

        Vector3 vel = Vector3.zero;
        do
        {
            vel.x = Random.value - 0.5f;
            vel.y = Random.value - 0.5f;
            vel.z = 0f;
        } while (vel == Vector3.zero);

        vel.Normalize();
        bulletBase.rigid.velocity = vel * bulletBase.bulletSpeed;

        return;
    }
}