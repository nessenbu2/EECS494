using UnityEngine;

public class AggroOnlyEnemy : EnemyBase
{
    [Header("BasicEnemy: Inspector Set Firing Fields")]
    public float fireRadius = 1f;

    // This virtual function fires this enemy's weapon.
    // Here in AggroOnlyEnemy, this simply fires directly
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
    // Here in the AggroOnlyEnemy, this always perform aggroMovement.
    protected override void move()
    {
        aggroMovement();
        return;
    }
}