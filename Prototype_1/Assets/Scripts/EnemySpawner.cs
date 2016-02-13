using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("EnemyBase: Inspector Set General Fields")]
    public List<GameObject> spawnPrefabs;
    public float spawnDelay = 4f;
    public int numberOfSpawns = 1;

    [Header("EnemyBase: Dynamically Set General Fields")]
    public float delayElapsedTime;

    void Awake()
    {
        ++EnemyBase.numEnemies;

        delayElapsedTime = 0f;
        return;
    }

    void Update()
    {
        if (numberOfSpawns <= 0)
        {
            --EnemyBase.numEnemies;

            Destroy(gameObject);
            return;
        }

        delayElapsedTime += Time.deltaTime;
        if (delayElapsedTime >= spawnDelay)
        {
            delayElapsedTime = 0f;
            --numberOfSpawns;

            GameObject go =
                Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Count)]);
            go.transform.position = transform.position;
        }

        return;
    }
}