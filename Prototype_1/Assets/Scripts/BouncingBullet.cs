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
        // Don't damage on reflection
        if (other.gameObject.tag == "Reflector")
        {
            return;
        }

        if (LayerMask.NameToLayer("Hero") == other.gameObject.layer)
        {
            Hero.hero.takeDamage(1);
            Destroy(gameObject);

            return;
        }

        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
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