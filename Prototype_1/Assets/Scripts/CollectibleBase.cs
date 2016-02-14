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
}
