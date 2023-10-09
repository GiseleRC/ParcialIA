using UnityEngine;

public class Arrival : SteeringAgent
{
    [SerializeField] Transform _targetReward;
    void Update()
    {
        //si no usa use avoidance aplica fuerza con el metodo arrive heredado de steering agent para direigirse hacia la reward
        if(!UseAvoidance()) AddForce(Arrive(_targetReward.position));
        Movement();
    }
}
