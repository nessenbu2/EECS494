using UnityEngine;
using System.Collections;

public class DefaultReflector : MonoBehaviour, IReflector
{
	GameObject bulletPrefab;

	public DefaultReflector(GameObject _bulletPrefab)
	{
		bulletPrefab = _bulletPrefab;
	}

	public void Reflect(Collider coll, Vector3 reflDir)
	{
		if (coll.attachedRigidbody && coll.gameObject.layer == LayerMask.NameToLayer("Bullet"))
		{
			GameObject refl = Instantiate<GameObject>(bulletPrefab);
			refl.gameObject.tag = "SpawnedBullet";
			refl.transform.position = coll.transform.position;

			Vector3 vel;
			vel = reflDir * coll.GetComponent<Rigidbody>().velocity.magnitude;

			refl.GetComponent<Rigidbody>().velocity = vel;

			BulletBase reflBase = refl.GetComponent<BulletBase>();
			reflBase.ignoreEnemies = false;
			reflBase.rend.material = reflBase.reflectMat;

			Destroy(coll.gameObject);
		}
		else if (coll.attachedRigidbody)
		{
			Vector3 vel;
			vel = reflDir * coll.GetComponent<Rigidbody>().velocity.magnitude;
			coll.GetComponent<Rigidbody>().velocity = vel;
		}
	}
}
