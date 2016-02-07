using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private float speed = 5f;
    private Rigidbody body;
    private HealthBar bar;

	public GameObject reflectorPrefab;
	float reflectorCooldown = 0.75f;
	float lastRefl;
	float spawnDist = 1f;

	void Start ()
    {
        bar = FindObjectOfType<HealthBar>();
	    body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        Move();

		if (Input.GetKey(KeyCode.Space) && Time.time - lastRefl > reflectorCooldown)
		{
			spawnReflector();
		}
	}

	private void spawnReflector()
	{
		lastRefl = Time.time;
		GameObject refl = Instantiate<GameObject>(reflectorPrefab);
		Vector3 pos = transform.position + transform.right * spawnDist;
		refl.transform.position = pos;
		refl.transform.rotation = transform.rotation;
	}

    private void Move()
    {
        Vector3 vel = body.velocity;

        if (Input.GetKey(KeyCode.W))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -speed;
        }
        else
        {
            vel.x = 0;
        }

        body.velocity = vel;
    }
}
