using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAgentWayPoint : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.allHunterAgentWayPoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.allHunterAgentWayPoints.Remove(this);
    }
}
