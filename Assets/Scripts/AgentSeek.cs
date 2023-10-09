using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSeek : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _maxSpeed, _maxForce;
    [SerializeField] float _radius;

    Vector3 _velocity;

    // Update is called once per frame
    void Update()
    {
        //Steering Behaviors

        //Fuerzas internas que impulsan a un objeto autonomo a ir hacia x direccion.

        //Vector Deseado (Zanahoria)

        //Vector de Steering = desired - velocity;

        //Locomotion AddForce

        Seek(_target.position);
        transform.position += _velocity * Time.deltaTime;
        transform.right = _velocity;
    }

    private void Seek(Vector3 targetPos)
    {
        Vector3 desired = targetPos - transform.position;
        desired.Normalize();
        desired *= _maxSpeed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
        //O podemos usar un valor super chiquito que vaya de 0 a 1 en _maxForce (flotantes chiquititos => 0.1f),
        //o utilizar un valor alto pero agregando la multiplicacion de _maxForce * time.deltaTime;

        AddForce(steering);
    }

    private void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

}
