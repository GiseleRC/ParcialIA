using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : SteeringAgent
{
    [SerializeField, Range(0f, 2.5f)] float _alignmentWeight = 1;
    [SerializeField, Range(0f, 2.5f)] float _separationWeight = 1;
    [SerializeField, Range(0f, 2.5f)] float _cohesionWeight = 1;

    private void Start()
    {
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f, 1f);

        var dir = new Vector3(x, y);

        _velocity = dir.normalized * _maxSpeed;

        GameManager.instance.allAgents.Add(this);
    }

    void Update()
    {
        Move();
        UpdateBoundPosition();
        Flocking();
    }

    private void Flocking()
    {
        var boids = GameManager.instance.allAgents;
        AddForce(Alignment(boids) * _alignmentWeight);
        AddForce(Separation(boids) * _separationWeight); //Se aplique un radio mas chico al actual
        AddForce(Cohesion(boids) * _cohesionWeight);
    }

    private void UpdateBoundPosition()
    {
        transform.position = GameManager.instance.AdjustPostionToBounds(transform.position);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
