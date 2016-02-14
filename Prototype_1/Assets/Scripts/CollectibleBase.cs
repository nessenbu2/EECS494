using UnityEngine;
using System.Collections;

public class CollectibleBase : MonoBehaviour {

	protected IReflector reflStrategy;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Hero"))
		{
			Hero.hero.transform.Find("Reflector").GetComponent<Reflector>().reflStrategy = reflStrategy;
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
