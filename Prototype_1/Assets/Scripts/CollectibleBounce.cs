using UnityEngine;
using System.Collections;

public class CollectibleBounce : CollectibleBase {

	public GameObject bulletPrefab;

	void Start () {
		reflStrategy = new BounceReflector(bulletPrefab);
	}
}
