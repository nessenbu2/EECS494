using UnityEngine;

public class BlindEnemy : EnemyBase
{

    // This virtual function fires this enemy's weapon.
    // Here in BlindEnemy, this fires in a random direction aimed at the poi.
    protected override void fire()
    {
        standardDirectShot();
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
