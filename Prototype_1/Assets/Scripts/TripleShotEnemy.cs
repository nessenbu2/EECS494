using UnityEngine;

public class TripleShotEnemy : EnemyBase
{
    [Header("BasicEnemy: Inspector Set Firing Fields")]
    public float fireRadius = 1f;
    public float forwardWeight = 1f;

    [Header("BasicEnemy: Inspector Set Movement Fields")]
    public float aggroRadius = 2f;

    // This virtual function fires this enemy's weapon.
    // Here in BasicEnemy, this simply fires directly
    // at the poi when within a certain radius.
    protected override void fire()
    {
        if (Vector3.Magnitude(poi.transform.position - transform.position) <=
            fireRadius)
        {
            directTripleShot(forwardWeight);
        }

        return;
    }

    // This virtual function moves this enemy.
    // Here in the BasicEnemy, this swaps between wander and aggro
    // depending on distance to the poi.
    protected override void move()
    {
        if (Vector3.Magnitude(poi.transform.position - transform.position) <=
            aggroRadius)
        {
            aggroMovement();
        }
        else
        {
            wanderMovement();
        }
        return;
    }
}