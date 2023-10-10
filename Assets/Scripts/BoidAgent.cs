using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : SteeringAgentClass
{
    //El boid utiliza movement, flockinBoid, UpdatePos, AdjustPositionToBounds

    // Rango de valores a lo ancho usados para aplicar floking, le dejamos valores inciales en 1 para que no queden en cero

    [SerializeField, Range(0f, 2.5f)] float _alignment = 1f;
    [SerializeField, Range(0f, 2.5f)] float _separation = 1f;
    [SerializeField, Range(0f, 2.5f)] float _cohesion = 1f;

    private void Start()
    {
        float xDir = Random.Range(-1f, 1f);
        float yDir = Random.Range(-1f, 1f);
        var director = new Vector3(xDir, yDir);

        //velocidad heredada de la clase steeringAgentBoid
        _velocity = director.normalized * _maxSpeed;

        GameManager.instance.allAgents.Add(this);
    }

    void Update()
    {
        //Funciones que se ejecutan continuamente
        Movement();
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

    //protected override void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, _viewRadius);
    //}
}
