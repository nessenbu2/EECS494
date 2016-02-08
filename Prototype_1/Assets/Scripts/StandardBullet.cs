using UnityEngine;

public class StandardBullet : BulletBase
{
    // For the standard bullet, the default behavior
    // is to destroy itself when it hits something.
    protected override void hitEntity(Collider other)
    {
        Destroy(gameObject);
        return;
    }
}