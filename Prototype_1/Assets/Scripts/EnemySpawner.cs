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
		enemy.transform.position = new Vector3(1,1,0);
		enemy.GetComponent<EnemyBase>().enemySpawn = this;
		print(enemy.GetComponent<EnemyBase>().enemySpawn);
	}
}
