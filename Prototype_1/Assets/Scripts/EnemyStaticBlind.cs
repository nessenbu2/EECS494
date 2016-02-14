using UnityEngine;

public class EnemyStaticBlind : EnemyBase
{
    [Header("EnemyStaticBlind: Dynamically Set Fields")]
    public Vector3 startLocation;

    protected override void onStart()
    {
        startLocation = transform.position;
        return;
    }

    // This virtual function fires this enemy's weapon.
    // Here in EnemyStaticBlind, this fires in a random direction.
    protected override void fire()
    {
        randomShot();
        return;
    }

    protected override void move()
    {
        transform.position = startLocation;
        return;
    }
}