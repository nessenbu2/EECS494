using UnityEngine;
using System.Collections;

public class CollectibleSides : CollectibleBase {

	override protected void applyEffect()
	{
		Hero.hero.addSides();
	}
}
