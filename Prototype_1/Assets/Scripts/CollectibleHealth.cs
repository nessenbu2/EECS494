using UnityEngine;
using System.Collections;

public class CollectibleHealth : CollectibleBase {

	public int health = 2;

	override protected void applyEffect()
	{
		Hero.hero.addHealth(health);
	}
}
