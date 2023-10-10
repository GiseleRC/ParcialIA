using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : SteeringAgent
{
    // Rango de valores a lo ancho usados para aplicar floking, le dejamos valores inciales en 1 para que no queden en cero

    [SerializeField, Range(0f, 2.5f)] protected float _alignment = 1f;
    [SerializeField, Range(0f, 2.5f)] protected float _separation = 1f;
    [SerializeField, Range(0f, 2.5f)] protected float _cohesion = 1f;

    private void Start()
    {
        Vector2 dir = Random.insideUnitCircle;
        var director = new Vector3(dir.x, 0f, dir.y);

        _velocity = director.normalized * _maxSpeed;

        GameManager.instance.allBoidAgents.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.allBoidAgents.Remove(this);
    }

    void Update()
    {
        HunterAgent nearestHunterAgent = null;

        foreach (HunterAgent hunterAgent in GameManager.instance.allHunterAgents)
        {
            if (!nearestHunterAgent || (hunterAgent.transform.position - transform.position).sqrMagnitude < (nearestHunterAgent.transform.position - transform.position).sqrMagnitude)
                nearestHunterAgent = hunterAgent;
        }

        if (nearestHunterAgent && (nearestHunterAgent.transform.position - transform.position).magnitude < _viewRadius)
        {
            if (!UseAvoidance()) AddForce(Flee(nearestHunterAgent.transform.position));
        }
        else
        {
            Reward nearestReward = null;

            foreach (Reward reward in GameManager.instance.allRewards)
            {
                if (!nearestReward || (reward.transform.position - transform.position).sqrMagnitude < (nearestReward.transform.position - transform.position).sqrMagnitude)
                    nearestReward = reward;
            }

            if (!UseAvoidance()) AddForce(Arrive(nearestReward.transform.position));
        }

        Move();
        FlockingBoid();
        UpdatePos();
    }

    private void UpdatePos()
    {
        transform.position = GameManager.instance.AdjustPositionToBounds(transform.position);
    }

    private void FlockingBoid()
    {
        var boidsAgents = GameManager.instance.allBoidAgents;

        //Al ejecutarse floking, agrego fuerza ejecutando los metodos de steerinAgentBoid, multiplicado la alineacion, cohesion y separacion 
        AddForce(AgentsAlignment(boidsAgents) * _alignment);
        AddForce(Separation(boidsAgents) * _separation);
        AddForce(Cohesion(boidsAgents) * _cohesion);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
