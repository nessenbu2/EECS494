using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {

	float initTime;
	float duration = 0.5f;

	// Update is called once per frame
	void Update () {
		if (Time.time - initTime >= duration)
			gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.attachedRigidbody)
		{
			// Redirect velocity to hero's x direction, same magnitude as before
			Vector3 dir = transform.rotation * Vector3.right;
			Vector3 vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude;

			coll.GetComponent<Rigidbody>().velocity = vel;
		}
	}

	public void initReflector()
	{
		gameObject.SetActive(true);
		initTime = Time.time;
	}
}
