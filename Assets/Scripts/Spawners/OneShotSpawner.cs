using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSpawner : Spawner
{
    [SerializeField] protected int count = 1;

    void Start()
    {
        Spawn();
    }

    protected override void Spawn()
    {
        for (int i = 0; i < count; ++i)
            SpawnInstance();
    }
}
