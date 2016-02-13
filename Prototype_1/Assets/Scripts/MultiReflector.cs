using UnityEngine;
using System.Collections;

public class MultiReflector : MonoBehaviour, IReflector {

	public GameObject bulletPrefab;

	public MultiReflector(GameObject _bulletPrefab)
	{
		bulletPrefab = _bulletPrefab;
	}

	public void Reflect(Collider coll)
	{
		if (coll.attachedRigidbody)
		{
			GameObject refl1, refl2, refl3;
			refl1 = Instantiate<GameObject>(bulletPrefab);
			refl2 = Instantiate<GameObject>(bulletPrefab);
			refl3 = Instantiate<GameObject>(bulletPrefab);

			refl1.gameObject.tag = "SpawnedBullet";
			refl2.gameObject.tag = "SpawnedBullet";
			refl3.gameObject.tag = "SpawnedBullet";

			refl1.transform.position = coll.transform.position;
			refl2.transform.position = coll.transform.position;
			refl3.transform.position = coll.transform.position;

			Vector3 dir1 = Hero.hero.transform.rotation * Vector3.right;
			Vector3 dir2 = Hero.hero.transform.rotation * Vector3.up;
			Vector3 dir3 = Hero.hero.transform.rotation * -Vector3.up;

			Vector3 vel1;
			Vector3 vel2;
			Vector3 vel3;
			//if (Time.time - initTime <= specialRefl) {
			//	vel1 = dir1 * coll.GetComponent<Rigidbody>().velocity.magnitude;
			//	vel2 = dir2 * coll.GetComponent<Rigidbody>().velocity.magnitude;
			//	vel3 = dir3 * coll.GetComponent<Rigidbody>().velocity.magnitude;
			//	refl1.GetComponent<Rigidbody>().velocity = vel1;
			//	refl2.GetComponent<Rigidbody>().velocity = vel2;
			//	refl3.GetComponent<Rigidbody>().velocity = vel3;
			//} else {
				vel1 = dir1 * coll.GetComponent<Rigidbody>().velocity.magnitude;
				refl1.GetComponent<Rigidbody>().velocity = vel1;
				vel2 = dir2 * coll.GetComponent<Rigidbody>().velocity.magnitude;
				refl2.GetComponent<Rigidbody>().velocity = vel1;
				vel3 = dir3 * coll.GetComponent<Rigidbody>().velocity.magnitude;
				refl3.GetComponent<Rigidbody>().velocity = vel1;
			//}

			Destroy(coll.gameObject);
		}
	}
}
