using UnityEngine;

public class BlindEnemy : EnemyBase
{
    // This virtual function fires this enemy's weapon.
    // Here in BlindEnemy, this fires in a random direction.
    protected override void fire()
    {
        elapsedFireTime += Time.deltaTime;
        randomShot();

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