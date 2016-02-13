using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour
{
	float specialRefl = 0.1f; // Might wanna make it lower
	public GameObject bulletPrefab;

	public IReflector reflStrategy;

	void Awake()
	{
		// reflStrategy = new DefaultReflector();
		reflStrategy = new MultiReflector(bulletPrefab);
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag != "SpawnedBullet")
		{
			Vector3 reflDir = transform.rotation * Vector3.right;
			reflStrategy.Reflect(coll, reflDir);
		}
	}

	public void initReflector()
	{
		//initTime = Time.time;
		gameObject.SetActive(true);
	}
}
