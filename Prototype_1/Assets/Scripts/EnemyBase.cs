using UnityEngine;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour
{
    [Header("EnemyBase: Inspector Set General Fields")]
    public int health = 1;
    public float invulnTime = 1.0f;
    public List<GameObject> powerupPrefabs;

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
    public bool isInvuln;
    public float invulnStartTime;

    [Header("EnemyBase: Dynamically Set General Firing Fields")]
    public float elapsedFireTime;

    [Header("EnemyBase: Dynamically Set General Movement Fields")]
    public Vector3 currentForce;
    public float elapsedForceShiftTime;
    public Vector3? collisionForce; // The ? makes this nullable.

    public static int numEnemies = 0; // I need to access in EnemySpawner.

    public static int NumEnemies()
    {
        return numEnemies;
    }

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

        isInvuln = false;
        numEnemies++;

        return;
    }

    void Start()
    {
        poi = Hero.hero.gameObject;
        return;
    }

    // Update will keep shooting as fluid as possible
    // keeping within a set timer.
    // This is a non-virtual interface allowing easy fire control.
    void Update()
    {
        if (poi == null)
        {
            return;
        }

        if (isInvuln && ((invulnStartTime + invulnTime) <= Time.time))
        {
            isInvuln = false;
        }

        elapsedFireTime += Time.deltaTime;
        if (elapsedFireTime < timeBetweenBulletShots)
        {
            return;
        }

        fire();
        return;
    }

    // FixedUpdate works best for movement,
    // so this non-virtual interface allows easy movement control.
    void FixedUpdate()
    {
        if (poi == null)
        {
            return;
        }

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
            if (isInvuln)
            {
                return;
            }
            isInvuln = true;
            invulnStartTime = Time.time;

            health -= bullet.bulletDamage;
            if (health <= 0)
            {
                if (powerupPrefabs.Count != 0)
                {
                    GameObject prefab =
                        powerupPrefabs[Random.Range(0, powerupPrefabs.Count)];
                    if (prefab != null)
                    {
                        GameObject powerup = Instantiate(prefab);
                        powerup.transform.position = transform.position;
                    }
                }

                onDeath();
                Destroy(gameObject);
            }
        }

        return;
    }

    void OnDestroy()
    {
        numEnemies--;
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
    protected void standardDirectShot()
    {
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

    // This functions shoots bullets in random directions.
    protected void randomShot()
    {
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

    protected void directedRandomShot(float minForwardWeight)
    {
        elapsedFireTime = 0f;

        GameObject goBullet = Instantiate(bullet);
        BulletBase bulletBase = goBullet.GetComponent<BulletBase>();

        goBullet.transform.position = transform.position;

        Vector3 vel = poi.transform.position - transform.position;
        vel.Normalize();

        Vector3 angle = Vector3.zero;
        if (Random.Range(0, 2) == 1)
        {
            angle = Vector3.Cross(vel, transform.forward);
        }
        else
        {
            angle = Vector3.Cross(transform.forward, vel);
        }

        float forwardWeight = Random.Range(minForwardWeight, 1f);
        vel = (forwardWeight * vel) + ((1 - forwardWeight) * angle);
        vel.Normalize();
        bulletBase.rigid.velocity = vel * bulletBase.bulletSpeed;

        return;
    }

    // This function shoots 3 bullets in an arc directed at the poi.
    // The "tightness" of the arc is defined by forwardWeight.
    protected void directTripleShot(float forwardWeight)
    {
        elapsedFireTime = 0f;

        GameObject centerBullet = Instantiate(bullet);
        GameObject leftBullet = Instantiate(bullet);
        GameObject rightBullet = Instantiate(bullet);

        BulletBase centerBase = centerBullet.GetComponent<BulletBase>();
        BulletBase leftBase = leftBullet.GetComponent<BulletBase>();
        BulletBase rightBase = rightBullet.GetComponent<BulletBase>();

        centerBullet.transform.position = transform.position;
        leftBullet.transform.position = transform.position;
        rightBullet.transform.position = transform.position;

        Vector3 centerVel = poi.transform.position - transform.position;
        centerVel.Normalize();
        Vector3 leftVel = (centerVel * forwardWeight) +
            Vector3.Cross(transform.forward, centerVel);
        leftVel.Normalize();
        Vector3 rightVel = (centerVel * forwardWeight) +
            Vector3.Cross(centerVel, transform.forward);
        rightVel.Normalize();

        centerBase.rigid.velocity = centerVel * centerBase.bulletSpeed;
        leftBase.rigid.velocity = leftVel * leftBase.bulletSpeed;
        rightBase.rigid.velocity = rightVel * rightBase.bulletSpeed;

        return;
    }

    // This function shoots 4 bullets in an arc directed at the poi.
    // The "tightness" of the arc is defined by the xForwardWeight params.
    // Usually, inner > outer (in order to keep it "inner").
    protected void directQuadShot(
        float innerForwardWeight, float outerForwardWeight)
    {
        elapsedFireTime = 0f;

        GameObject outerLeftBullet = Instantiate(bullet);
        GameObject innerLeftBullet = Instantiate(bullet);
        GameObject innerRightBullet = Instantiate(bullet);
        GameObject outerRightBullet = Instantiate(bullet);

        BulletBase outerLeftBase = outerLeftBullet.GetComponent<BulletBase>();
        BulletBase innerLeftBase = innerLeftBullet.GetComponent<BulletBase>();
        BulletBase innerRightBase = innerRightBullet.GetComponent<BulletBase>();
        BulletBase outerRightBase = outerRightBullet.GetComponent<BulletBase>();

        outerLeftBullet.transform.position = transform.position;
        innerLeftBullet.transform.position = transform.position;
        innerRightBullet.transform.position = transform.position;
        outerRightBullet.transform.position = transform.position;

        Vector3 forward = poi.transform.position - transform.position;
        forward.Normalize();

        Vector3 outerLeftVel = (forward * outerForwardWeight) +
            Vector3.Cross(transform.forward, forward);
        Vector3 innerLeftVel = (forward * innerForwardWeight) +
            Vector3.Cross(transform.forward, forward);
        Vector3 innerRightVel = (forward * innerForwardWeight) +
            Vector3.Cross(forward, transform.forward);
        Vector3 outerRightVel = (forward * outerForwardWeight) +
            Vector3.Cross(forward, transform.forward);

        outerLeftVel.Normalize();
        innerLeftVel.Normalize();
        innerRightVel.Normalize();
        outerRightVel.Normalize();

        outerLeftBase.rigid.velocity =
            outerLeftVel * outerLeftBase.bulletSpeed;
        innerLeftBase.rigid.velocity =
            innerLeftVel * innerLeftBase.bulletSpeed;
        innerRightBase.rigid.velocity =
            innerRightVel * innerRightBase.bulletSpeed;
        outerRightBase.rigid.velocity =
            outerRightVel * outerRightBase.bulletSpeed;

        return;
    }
}
