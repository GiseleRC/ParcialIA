using UnityEngine;

public class NPCHunterAgent : SteeringAgent
{
    [SerializeField] SteeringAgent _targetBoidAgent;
    [SerializeField] float _distanceToHunt;

    //El hunter utiliza UseAvoidanse, addforce, pursuit, movement, hunt
    void Update()
    {
        if (!UseAvoidance()) AddForce(Pursuit(_targetBoidAgent));
        Move();

        Hunt();
    }

    private void Hunt()
    {
        if (Vector3.Distance(transform.position, _targetBoidAgent.transform.position) <= _distanceToHunt)
        {
             _targetBoidAgent.ResetVector();
        }
    }
}
