using UnityEngine;

// This enemy name is likely to change, but it is mostly a testbed enemy.
public class BasicEnemy : EnemyBase
{
    [Header("BasicEnemy: Inspector Set Firing Fields")]
    public float fireRadius = 1f;

    // This virtual function fires this enemy's weapon.
    // Here in BasicEnemy, this simply fires directly
    // at the poi when within a certain radius.
    protected override void fire()
    {
        if (Vector3.Magnitude(poi.transform.position - transform.position) <=
            fireRadius)
        {
            standardDirectShot();
        }
        return;
    }

    // This virtual function moves this enemy.
    // Here in the BasicEnemy, this swaps between wander and aggro
    // depending on distance to the poi.
    protected override void move()
    {
        wanderMovement();
        return;
    }
}