using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {

	float initTime;
	float specialRefl = 0.1f; // Might wanna make it lower

	public IReflector reflStrategy;

	void Awake()
	{
		reflStrategy = new DefaultReflector();
	}

	// Update is called once per frame
	void Update () {
		//if (Time.time - initTime >= duration)
		//	gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider coll) {
		reflStrategy.Reflect(coll, initTime);
		if (coll.attachedRigidbody)
		{
			// Redirect velocity to hero's x direction, same magnitude as before
			Vector3 dir = transform.rotation * Vector3.right;

			Vector3 vel;
			if (Time.time - initTime <= specialRefl) {
				vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude * 2;
			} else {
				vel = dir * coll.GetComponent<Rigidbody>().velocity.magnitude;
			}

			coll.GetComponent<Rigidbody>().velocity = vel;
		}
	}

	public void initReflector()
	{
		initTime = Time.time;
		gameObject.SetActive(true);
	}
}
