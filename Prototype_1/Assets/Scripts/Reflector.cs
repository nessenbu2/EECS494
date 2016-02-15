using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour
{
	float specialRefl = 0.1f; // Might wanna make it lower
	public GameObject bulletPrefab;

	public IReflector reflStrategy;
	public IReflector defaultRefl;

	void Awake()
	{
		reflStrategy = new DefaultReflector();
		defaultRefl = new DefaultReflector();
	}

	virtual protected void OnTriggerEnter(Collider coll) {
		Vector3 reflDir = transform.rotation * Vector3.right;
		print (reflDir);
		if (coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			defaultRefl.Reflect(coll, reflDir);
		}
		else if (coll.gameObject.tag != "SpawnedBullet")
		{
			reflStrategy.Reflect(coll, reflDir);
		}
	}

	public void initReflector()
	{
		//initTime = Time.time;
		gameObject.SetActive(true);
	}
}
