using UnityEngine;

// Having this base class will allows us
// to add different types of bullets later.
public class BulletBase : MonoBehaviour
{
    [Header("BulletBase: Inspector Set Fields")]
    public float bulletSpeed = 2.0f;
    public int bulletDamage = 1;

    [Header("BulletBase: Dynamically Set Fields")]
    public GameObject originEnemy;

    // This will deal with the bullet hitting other entities.
    // It will not be able to damage the originating enemy.
    // This is a non-virtual interface so that some bullets
    // are allowed to have different properties (e.g. bouncing off walls).
    void OnTriggerEnter(Collider other)
    {
        // This should probably be done with a tag, but this works for now.
        if (other.gameObject == originEnemy)
        {
            return;
        }
        
        hitEntity(other);
    }

    // This virtual method allows different bullets to be made
    // that deal different with hitting another entity.
    // The default is to do nothing.
    protected virtual void hitEntity(Collider other)
    {
        return;
    }
}