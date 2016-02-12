using UnityEngine;
using System.Collections;

public class MultiReflector : IReflector {

	public MultiReflector() {}
	public void Reflect(Collider coll)
	{
		//if (coll.attachedRigidbody)
		//{
		//	GameObject refl1, refl2, refl3;
		//	refl1 = Instantiate<GameObject>(bulletPrefab);
		//	refl2 = Instantiate<GameObject>(bulletPrefab);
		//	refl3 = Instantiate<GameObject>(bulletPrefab);

		//	Vector3 dir1 = transform.rotation * Vector3.right;
		//	Vector3 dir2 = transform.rotation * Vector3.up;
		//	Vector3 dir3 = transform.rotation * -Vector3.up;

		//	Vector3 vel1;
		//	Vector3 vel2;
		//	Vector3 vel3;
		//	if (Time.time - initTime <= specialRefl) {
		//		vel1 = dir1 * coll.GetComponent<Rigidbody>().velocity.magnitude;
		//		vel2 = dir2 * coll.GetComponent<Rigidbody>().velocity.magnitude;
		//		vel3 = dir3 * coll.GetComponent<Rigidbody>().velocity.magnitude;
		//		refl1.GetComponent<Rigidbody>().velocity = vel1;
		//		refl2.GetComponent<Rigidbody>().velocity = vel2;
		//		refl3.GetComponent<Rigidbody>().velocity = vel3;
		//	} else {
		//		vel1 = dir1 * coll.GetComponent<Rigidbody>().velocity.magnitude;
		//		refl1.GetComponent<Rigidbody>().velocity = vel1;
		//	}

		//	Destroy(coll);
		//}
	}
}
