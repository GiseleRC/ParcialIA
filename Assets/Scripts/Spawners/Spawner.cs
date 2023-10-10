using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform parent;
    [SerializeField] protected Transform[] spawnPoints;

    protected int spawnPointIdx = 0;

    protected abstract void Spawn();

    protected virtual void ShuffleSpawnPoints()
    {
        for (int i = spawnPoints.Length - 1; i > 0; --i)
        {
            int rnd = Random.Range(0, i);
            Transform tmp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[rnd];
            spawnPoints[rnd] = tmp;
        }
    }

    protected virtual void SpawnInstance(Transform spawnPoint)
    {
        GameObject.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, parent);
    }

    protected virtual void SpawnInstance()
    {
        SpawnInstance(spawnPoints[spawnPointIdx]);
        if (++spawnPointIdx >= spawnPoints.Length) spawnPointIdx = 0;
    }
}
