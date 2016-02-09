using UnityEngine;

public class StandardBullet : BulletBase
{
    // For the standard bullet, the default behavior
    // is to destroy itself when it hits something.
    protected override void hitEntity(Collider other)
    {
		// Don't damage on reflection
		if (other.gameObject.tag == "Reflector")
		{
			return;
		}

		if (LayerMask.NameToLayer("Hero") == other.gameObject.layer)
		{
			Hero.hero.takeDamage(1);
		}

		if (LayerMask.NameToLayer("Reflector") != other.gameObject.layer
				&& LayerMask.NameToLayer("Bullet") != other.gameObject.layer)
		{
			Destroy(gameObject);
			return;
		}
    }
}