using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalAgent : SteeringAgent
{
    [SerializeField] Transform _target;
    //Solo queremos usar Arrive y Obstacle Avoidance

    void Update()
    {
        if(!HastToUseObstacleAvoidance()) AddForce(Arrive(_target.position));
        Move();
    }

    protected override void OnDrawGizmos()
    {
        //base.OnDrawGizmos();
    }
}
