using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 0.5f);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider coll) {
		// x direction of the reflector is away from hero
		Vector3 dir = transform.rotation * Vector3.right;
		if (coll.attachedRigidbody)
		{
			coll.attachedRigidbody.AddForce(dir * 40, ForceMode.Impulse);
		}
	}
}
