using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("EnemyBase: Inspector Set General Fields")]
    public GameObject poi;

    [Header("EnemyBase: Inspector Set General Movement Fields")]
    public float timeBetweenForceShift = 1.0f;
    public float newForceWeight = 0.1f;
    public float maxSpeed = 2.0f;

    [Header("EnemyBase: Dynamically Set Sub-Objects")]
    public Rigidbody rigid;

    [Header("EnemyBase: Dynamically Set General Movement Fields")]
    public Vector3 currentForce;
    public float elapsedForceShiftTime;
    public Collision currentCollision;

    // Initialize all needed starting values.
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        currentForce = Vector3.zero;

        // Ensure that the force "shifts" immediately,
        // i.e. give it an initial random force.
        elapsedForceShiftTime = timeBetweenForceShift;
        currentCollision = null;

        return;
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
        currentCollision = collision;
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

    // This function performs the wander movement action.
    // The enemy moves in a fluid manner in random directions.
    // This is assumed to only ever be called through FixedUpdate().
    protected void wanderMovement()
    {
        if (currentCollision != null)
        {
            currentForce = Vector3.Reflect(
                currentForce, currentCollision.contacts[0].normal);

            currentForce.Normalize();
            currentForce *= maxSpeed;

            currentCollision = null;
            elapsedForceShiftTime = 0f;
        }

        elapsedForceShiftTime += Time.deltaTime;

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
    // The enemy moves toward the poi up to a certain radius.
    // At this radius, the enemy will attempt to circle the poi.
    // This is assumed to only ever be called through FixedUpdate().
    protected void aggroMovement()
    {
        return;
    }

    // This function performs a standard direct shot at the poi.
    // The enemy shoots directly at the poi.
    // This is assumed to only ever be called through Update().
    protected void standardDirectShot()
    {

    }
}