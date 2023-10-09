using UnityEngine;

public class NPCHunter : SteeringAgent
{
    [SerializeField] SteeringAgent _targetBoidAgent;
    [SerializeField] float _distanceToHunt;

    void Update()
    {
        if (!UseAvoidance()) AddForce(Pursuit(_targetBoidAgent));
        Movement();

        Hunt();
    }

    private void Hunt()
    {
        if (Vector3.Distance(transform.position, _targetBoidAgent.transform.position) <= _distanceToHunt)
        {
             _targetBoidAgent.ResetPos();
        }
        
    }
}
