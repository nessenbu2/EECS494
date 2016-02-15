using UnityEngine;

// Having this base class will allows us
// to add different types of bullets later.
public class BulletBase : MonoBehaviour
{
    [Header("BulletBase: Inspector Set Fields")]
    public float bulletSpeed = 2.0f;
    public int bulletDamage = 1;

    public Material enemyMat;
    public Material reflectMat;

    [Header("BulletBase: Dynamically Set Fields")]
    public Rigidbody rigid;
    public MeshRenderer rend;

    public bool ignoreEnemies;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();

        ignoreEnemies = true;
        rend.material = enemyMat;

        return;
    }

    void Start()
    {
        onStart();
        return;
    }

    void Update()
    {
        onUpdate();
        return;
    }

    void FixedUpdate()
    {
        onFixedUpdate();
        return;
    }

    // This will deal with the bullet hitting other entities.
    // It will not be able to damage the originating enemy.
    // This is a non-virtual interface so that some bullets
    // are allowed to have different properties (e.g. bouncing off walls).
    void OnTriggerEnter(Collider other)
    {
        // Don't damage on reflection
        if (other.gameObject.tag == "Reflector")
        {
            rend.material = reflectMat;
            ignoreEnemies = false;
            return;
        }

        if ((LayerMask.NameToLayer("Enemy") == other.gameObject.layer) &&
            ignoreEnemies)
        {
            return;
        }

        if (LayerMask.NameToLayer("Hero") == other.gameObject.layer)
        {
            Hero.hero.takeDamage(1);
            Destroy(gameObject);

            return;
        }

        if (LayerMask.NameToLayer("Bullet") == other.gameObject.layer)
        {
            return;
        }

        hitEntity(other);
        return;
    }

    // This virtual method allows for varying initializations.
    // The default is to do nothing.
    protected virtual void onStart()
    {
        return;
    }

    // This virtual method allows for varying onUpdate actions.
    // The default is to do nothing.
    protected virtual void onUpdate()
    {
        return;
    }

    // This virtual method allows for varying onFixedUpdate actions.
    // The default is to do nothing.
    protected virtual void onFixedUpdate()
    {
        return;
    }

    // This virtual method allows different bullets to be made
    // that deal different with hitting another entity.
    // The default is to do nothing.
    protected virtual void hitEntity(Collider other)
    {
        return;
    }
}