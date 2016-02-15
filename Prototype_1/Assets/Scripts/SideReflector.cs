using UnityEngine;
using System.Collections;

public class SideReflector : Reflector {

	void Awake()
	{
		reflStrategy = new DefaultReflector();
	}

	override protected void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Default"))
		{
			return;
		}
		base.OnTriggerEnter(coll);
		gameObject.SetActive(false);
	}
}
