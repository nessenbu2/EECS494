using UnityEngine;
using System.Collections;

public class CollectibleMulti : CollectibleBase
{
	public GameObject bulletPrefab;

	void Start()
	{
		reflStrategy = new MultiReflector(bulletPrefab);
	}
}
