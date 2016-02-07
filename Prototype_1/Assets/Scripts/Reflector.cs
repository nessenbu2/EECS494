using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 0.5f);
	}

	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.rotation * Vector3.right, transform.rotation * Vector3.right, Color.red, Mathf.Infinity);
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
}
