using UnityEngine;

public class BouncingBullet : BulletBase
{
    [Header("BouncingBullet: Inspector Set Fields")]
    public float bulletTimeToLive = 5.0f;

    [Header("BouncingBullet: Dynamically Set Fields")]
    public float bulletAliveTime = 0f;

    protected override void onStart()
    {
        bulletAliveTime = 0f;
        return;
    }

    protected override void onUpdate()
    {
        bulletAliveTime += Time.deltaTime;
        return;
    }

    // The bouncing bullet bounces off walls and the reflector,
    // hitting anything else destroys it.
    protected override void hitEntity(Collider other)
    {
        if ((LayerMask.NameToLayer("Enemy") == other.gameObject.layer) &&
            !ignoreEnemies)
        {
            Destroy(gameObject);
            return;
        }

        if (bulletAliveTime >= bulletTimeToLive)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 vel = rigid.velocity;
        vel.Normalize();

        RaycastHit hit;
        Vector3 movedPos = transform.position - (vel * 0.5f);
        Physics.Raycast(movedPos, vel, out hit, 2f);

        vel = Vector3.Reflect(vel, hit.normal);
        vel.Normalize();
        rigid.velocity = vel * bulletSpeed;

        return;
    }
}