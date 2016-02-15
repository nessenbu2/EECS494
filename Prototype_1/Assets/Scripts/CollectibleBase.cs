using UnityEngine;
using System.Collections;

public class CollectibleBase : MonoBehaviour {

	protected IReflector reflStrategy;

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
