using UnityEngine;
using System.Collections.Generic;

public class EnemyQueen : EnemyBase
{
    [Header("EnemyQueen: Inspector Set Fields")]
    public List<GameObject> spawnPrefabs;
    public int shotsBetweenSpawns = 5;
    public int spawnsAfterDeath = 10;

    public float innerForwardWeight = 2f;
    public float outerForwardWeight = 1f;

    [Header("EnemyStaticBlind: Dynamically Set Fields")]
    public Vector3 startLocation;
    public int shotCount;

    protected override void onStart()
    {
        startLocation = transform.position;
        shotCount = 0;

        return;
    }

    protected override void fire()
    {
        if (shotCount++ == 0)
        {
            spawnEnemy(spawnPrefabs);
            return;
        }

        directQuadShot(innerForwardWeight, outerForwardWeight);
        if (shotCount >= shotsBetweenSpawns)
        {
            shotCount = 0;
        }

        return;
    }

    protected override void move()
    {
        transform.position = startLocation;
        return;
    }

    protected override void onDeath()
    {
        for (int i = 0; i < spawnsAfterDeath; ++i)
        {
            spawnEnemy(spawnPrefabs);
        }
        return;
    }
}