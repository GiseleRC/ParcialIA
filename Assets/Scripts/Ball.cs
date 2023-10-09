using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 _velocity;
    [SerializeField] float _maxSpeed, _maxForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //transform.position += -transform.right * Time.deltaTime;
            //_velocity += Vector3.left;
            AddForce(Vector3.left * _maxForce * Time.deltaTime);
        }

        //transform.position += -transform.up * Time.deltaTime;
        //_velocity += Vector3.down;
        //_velocity.Normalize();
        //_velocity = _velocity.normalized;

        AddForce(Vector3.down * _maxForce * Time.deltaTime);

        transform.position += _velocity * Time.deltaTime;

        Bounce();
    }

    private void Bounce()
    {
        var lowerPos = transform.position + Vector3.down / 2;
        if(lowerPos.y <= 0 && _velocity.y < 0)
        {
            _velocity *= -1;
        }
    }

    private void AddForce(Vector3 force)
    {
        //_velocity += force;
        //_velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, Vector3.right * 15f);
    }
}
