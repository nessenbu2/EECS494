// This enemy name is likely to change, but it is mostly a testbed enemy.
public class BasicEnemy : EnemyBase
{
    // This virtual function moves this enemy.
    // Here in the BasicEnemy, this swaps between wander and aggro
    // depending on distance to the poi (player character).
    protected override void move()
    {
        wanderMovement();
        return;
    }
}