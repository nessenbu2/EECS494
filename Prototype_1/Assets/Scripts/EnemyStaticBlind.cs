public class EnemyStaticBlind : EnemyBase
{
    // This virtual function fires this enemy's weapon.
    // Here in EnemyStaticBlind, this fires in a random direction.
    protected override void fire()
    {
        randomShot();
        return;
    }
}