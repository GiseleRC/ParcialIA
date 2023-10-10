using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
   private void Start()
    {
        GameManager.instance.allRewards.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.allRewards.Remove(this);
    }
}
