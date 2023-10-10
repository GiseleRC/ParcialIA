using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : SteeringAgent
{
    //El boid utiliza movement, flockinBoid, UpdatePos, AdjustPositionToBounds

    // Rango de valores a lo ancho usados para aplicar floking, le dejamos valores inciales en 1 para que no queden en cero

    [SerializeField, Range(0f, 2.5f)] protected float _alignment = 1f;
    [SerializeField, Range(0f, 2.5f)] protected float _separation = 1f;
    [SerializeField, Range(0f, 2.5f)] protected float _cohesion = 1f;

    private void Start()
    {
        Vector2 dir = Random.insideUnitCircle;
        var director = new Vector3(dir.x, 0f, dir.y);

        //velocidad heredada de la clase steeringAgentBoid
        _velocity = director.normalized * _maxSpeed;

        GameManager.instance.allAgents.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.instance.allAgents.Remove(this);
    }

    void Update()
    {
        Reward nearestReward = null;

        foreach (Reward reward in GameManager.instance.allRewards)
        {
            if (!nearestReward || (reward.transform.position - transform.position).sqrMagnitude < (nearestReward.transform.position - transform.position).sqrMagnitude)
                nearestReward = reward;
        }

        //Funciones que se ejecutan continuamente
        if (!UseAvoidance()) AddForce(Arrive(nearestReward.transform.position));
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
        var boidsAgents = GameManager.instance.allAgents;

        //Al ejecutarse floking, agrego fuerza ejecutando los metodos de steerinAgentBoid, multiplicado la alineacion, checion y separacion 
        AddForce(AgentsAlignment(boidsAgents) * _alignment);
        AddForce(Separation(boidsAgents) * _separation);
        AddForce(Cohesion(boidsAgents) * _cohesion);
    }

    //NOSE SI SIRVE EJECUTAR ESTE METODO

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
