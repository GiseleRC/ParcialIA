using UnityEngine;

public class Arrival : SteeringAgentClass
{
    [SerializeField] Transform _targetReward;
    void Update()
    {
        //si no usa use avoidance aplica fuerza con el metodo arrive heredado de steering agent para dirigirse hacia la reward
        if(!UseAvoidance()) AddForce(Arrive(_targetReward.position));
        Movement();
    }
}
