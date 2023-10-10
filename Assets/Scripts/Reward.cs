using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] protected LayerMask _boidAgentsMask;

    private void Start()
    {
        GameManager.instance.allRewards.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.allRewards.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_boidAgentsMask & 1 << other.gameObject.layer) == 0) return;

        transform.position = GameManager.instance.GetObstacleFreePosition();
    }
}
