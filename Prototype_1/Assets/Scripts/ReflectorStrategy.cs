using UnityEngine;

public interface IReflector {
	protected float specialRefl = 0.1f;

	void Reflect(Collider coll);
}
