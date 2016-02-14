using UnityEngine;
using System.Collections;

public class BounceReflector : MonoBehaviour, IReflector
{
	public GameObject bulletPrefab;

	public BounceReflector(GameObject _bulletPrefab)
	{
		bulletPrefab = _bulletPrefab;
	}

	public void Reflect(Collider coll, Vector3 reflDir)
	{
		if (coll.attachedRigidbody && coll.gameObject.layer == LayerMask.NameToLayer("Bullet"))
		{
			GameObject refl;
			refl = Instantiate<GameObject>(bulletPrefab);
			refl.gameObject.tag = "SpawnedBullet";
			refl.transform.position = coll.transform.position;

			Vector3 dir = reflDir;
			Vector3 vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude;
			refl.GetComponent<Rigidbody>().velocity = vel;
			Destroy(coll.gameObject);
		}
	}
}
