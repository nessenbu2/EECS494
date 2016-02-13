using UnityEngine;
using System.Collections;

public class DefaultReflector : IReflector
{
	public DefaultReflector() {}

	public void Reflect(Collider coll, Vector3 reflDir)
	{
		if (coll.attachedRigidbody)
		{
			Vector3 vel;
			// if (Time.time - initTime <= specialRefl) {
			// 	vel = reflDir * coll.GetComponent<Rigidbody>().velocity.magnitude * 2;
			// } else {
			 	vel = reflDir * coll.GetComponent<Rigidbody>().velocity.magnitude;
			// }

			coll.GetComponent<Rigidbody>().velocity = vel;
		}
	}
}
