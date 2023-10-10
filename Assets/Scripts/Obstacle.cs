using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.allObstacles.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.allObstacles.Remove(this);
    }
}
