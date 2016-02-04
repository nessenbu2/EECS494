﻿using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private float speed = 5f;
    private Rigidbody body;

	// Use this for initialization
	void Start () {
	    body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
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
