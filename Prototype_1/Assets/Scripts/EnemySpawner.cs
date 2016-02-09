using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject enemy;

	// Update is called once per frame
	void Update () {
		if (enemy)
			return;

		enemy = Instantiate<GameObject>(enemyPrefab);
		enemy.transform.position = transform.position;
		enemy.GetComponent<EnemyBase>().enemySpawn = this;
	}
}
