using UnityEngine;

public class BlindEnemy : EnemyBase
{
    [Header("EnemyBase: Inspector Set General Fields")]
    public float minForwardWeight = 0.5f;

    // This virtual function fires this enemy's weapon.
    // Here in BlindEnemy, this fires in a random direction.
    protected override void fire()
    {
        directedRandomShot(minForwardWeight);
        return;
    }

    // This virtual function moves this enemy.
    // Here in the BlindEnemy, this only ever wanders.
    protected override void move()
    {
        wanderMovement();
        return;
    }
}