using UnityEngine;

public class HomingBullet : BulletBase
{
    [Header("HomingBullet: Inspector Set Fields")]
    public float weightToPoi = 0.1f;

    [Header("HomingBullet: Dynamically Set Fields")]
    public GameObject poi;

    protected override void onStart()
    {
        poi = Hero.hero.gameObject;
    }

    protected override void onFixedUpdate()
    {
        if ((Hero.hero == null) && (poi != gameObject))
        {
            poi = gameObject;
        }

        Vector3 vel = rigid.velocity;
        vel.Normalize();

        Vector3 toPoi = poi.transform.position - transform.position;
        toPoi.Normalize();

        vel = ((1 - weightToPoi) * vel) + (weightToPoi * toPoi);
        rigid.velocity = vel * bulletSpeed;

        return;
    }

    // For the homing bullet, the default behavior
    // is to destroy itself when it hits something.
    protected override void hitEntity(Collider other)
    {
        Destroy(gameObject);
        return;
    }
}