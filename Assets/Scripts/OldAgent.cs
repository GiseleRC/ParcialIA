using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAgent : MonoBehaviour
{
    [SerializeField] Transform _cube;
    [SerializeField] float _speed;
    [SerializeField] float _radius;

    void Update()
    {
        Vector3 dir = _cube.position - transform.position; //Vector Director

        if (dir.magnitude > _radius)
        {
            transform.position += dir.normalized * _speed * Time.deltaTime;
        }
        else Debug.Log("Shot Time");

        transform.right = dir;

        //Mas caro, mas legible. Si ya sacamos el calculo de vector Director nos combiene usar magnitude, sino el distance
        //por la legibilidad.
        /* if (Vector3.Distance(transform.position, _cube.position) > _radius)
         {
             transform.position += dir.normalized * _speed * Time.deltaTime;
         }
         else Debug.Log("Shot Time");*/


        //En terminos de memoria es muchisimo mas barato pero mas ilegible 
        /*if (dir.sqrMagnitude > _radius * _radius)
        {
            transform.position += dir.normalized * _speed * Time.deltaTime;
        }
        else Debug.Log("Shot Time");*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


}
