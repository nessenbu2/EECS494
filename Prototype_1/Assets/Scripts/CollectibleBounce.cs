using UnityEngine;
using System.Collections;

public class CollectibleBounce : CollectibleBase {

	public GameObject bulletPrefab;

	void Start () {
		reflStrategy = new BounceReflector(bulletPrefab);
	}

	override protected void applyEffect()
	{
		Hero.hero.transform.Find("Reflector").GetComponent<Reflector>().reflStrategy = reflStrategy;
	}
}
