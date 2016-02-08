using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
    public GameObject hero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = Vector3.zero;
        temp.x = hero.transform.position.x;
        temp.y = hero.transform.position.y;
        temp.z = -20;
        transform.position = temp;
        
        //pos.y
	}
}
