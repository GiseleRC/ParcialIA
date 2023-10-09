using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSeekFleeYArrive : MonoBehaviour
{
    [SerializeField] Transform _seekTarget, _fleeTarget;
    [SerializeField] float _maxSpeed, _maxForce;
    [SerializeField] float _viewRadius;

    Vector3 _velocity;

    void Update()
    {
        //Optimo, pero poco legible
        // Vector3 dir = _fleeTarget.position - transform.position;
        // float dist = dir.magnitude;
        //if ((_fleeTarget.position - transform.position).magnitude <= _viewRadius)

        //Aun mas optimicidad, pero aun ams ilegible
        //if ((_fleeTarget.position - transform.position).sqrMagnitude <= _viewRadius * _viewRadius)

        //Mas legible, pero un poquito menos optimo

        //bool stopMyMovement = Vector3.Distance(transform.position, _seekTarget.position) <= _closeRadius;

        if(Vector3.Distance(transform.position, _fleeTarget.position) <= _viewRadius)
        {
            AddForce(Flee(_fleeTarget.position));
        }
        else 
        {
            //AddForce(Seek(_seekTarget.position));
            //if (stopMyMovement) _velocity = Vector3.zero;
            AddForce(Arrive(_seekTarget.position));
        }

        transform.position += _velocity * Time.deltaTime;
        if(_velocity != Vector3.zero) transform.right = _velocity;
    }

    Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxSpeed);
    }

    Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = (targetPos - transform.position).normalized * speed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    Vector3 Flee(Vector3 targetPos)
    {
        return -Seek(targetPos);
    }

    Vector3 Arrive(Vector3 targetPos)
    {
        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist > _viewRadius) return Seek(targetPos);

       return Seek(targetPos, _maxSpeed * (dist / _viewRadius));

    }

    private void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

    }
}
