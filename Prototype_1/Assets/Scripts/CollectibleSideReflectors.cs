using UnityEngine;
using System.Collections;

public class CollectibleSideReflectors : CollectibleBase {

	override protected void applyEffect()
	{
		Hero.hero.addSides();
	}
}
