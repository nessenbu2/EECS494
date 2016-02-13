using UnityEngine;
using System.Collections;

public class DefaultReflector : IReflector
{
	public DefaultReflector() {}

	public void Reflect(Collider coll)
	{
		if (coll.attachedRigidbody)
		{
			// Redirect velocity to hero's x direction, same magnitude as before
			Vector3 dir = Hero.hero.transform.rotation * Vector3.right;

			Vector3 vel;
			// if (Time.time - initTime <= specialRefl) {
			// 	vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude * 2;
			// } else {
			 	vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude;
			// }

			coll.GetComponent<Rigidbody>().velocity = vel;
		}
	}
}
