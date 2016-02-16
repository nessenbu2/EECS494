using UnityEngine;
using System.Collections;

public class CollectibleBase : MonoBehaviour {

	protected IReflector reflStrategy;

	void Awake()
	{
		Destroy(gameObject, 5);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Hero"))
		{
			applyEffect();
			Destroy(gameObject);
		}
	}

	void Update()
	{
		transform.Rotate(0,0,50 * Time.deltaTime);
	}

	virtual protected void applyEffect() {}
}
