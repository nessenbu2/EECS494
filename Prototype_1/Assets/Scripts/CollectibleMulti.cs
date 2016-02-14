using UnityEngine;
using System.Collections;

public class CollectibleMulti : CollectibleBase
{
	public GameObject bulletPrefab;

	void Start()
	{
		reflStrategy = new MultiReflector(bulletPrefab);
	}

	override protected void applyEffect()
	{
		Hero.hero.transform.Find("Reflector").GetComponent<Reflector>().reflStrategy = reflStrategy;
	}
}
