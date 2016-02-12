using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour {

	public GameObject bulletPrefab;
	public GameObject target;

	public float cooldown; // time between bullets
	public float bulletSpeed;
	float lastFired;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Vector3 targetDir = (transform.position - target.transform.position).normalized;
		spawn();
	}

	void spawn() {
		if (Time.time - lastFired < cooldown)
			return;

		lastFired = Time.time;
		GameObject bullet = Instantiate<GameObject>(bulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.LookAt(target.transform);
		Vector3 vel = bullet.transform.rotation * new Vector3(0,0,bulletSpeed);
		bullet.GetComponent<Rigidbody>().velocity = vel;
	}
}
