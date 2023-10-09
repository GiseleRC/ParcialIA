using UnityEngine;

public class AgentObstacleAvoidance : MonoBehaviour
{
    [SerializeField] Transform _seekTarget, _fleeTarget;
    [SerializeField] float _maxSpeed, _maxForce;
    [SerializeField] float _viewRadius;
    [SerializeField] LayerMask _obstacles;

    Vector3 _velocity;

    void Update()
    {
        Vector3 avoidanceObs = ObstacleAvoidance();

        if(avoidanceObs != Vector3.zero)
        {
            AddForce(avoidanceObs);
        }
        else if (Vector3.Distance(transform.position, _fleeTarget.position) <= _viewRadius)
        {
            AddForce(Flee(_fleeTarget.position));
        }
        else
        {
            AddForce(Arrive(_seekTarget.position));
        }

        transform.position += _velocity * Time.deltaTime;
        if (_velocity != Vector3.zero) transform.right = _velocity;
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

    Vector3 ObstacleAvoidance()
    {
        //if(Physics.Raycast(transform.position, transform.right, _viewRadius, 6))//Tomame todas las layers hasta la 6 incluida
        //if(Physics.Raycast(transform.position, transform.right, _viewRadius, 1 << 6))//Tomame una layer que sea la 6
        /*if (Physics.Raycast(transform.position, transform.right, _viewRadius, _obstacles))//Punto de inicio, direccion, largo y layermask
        {
            Vector3 desired = transform.position - transform.up;
            Debug.Log("Vi un obstaculo");
            return Seek(desired);
        }
        else */
     
        /*if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacles))//Punto de inicio, direccion, largo y layermask
        {
            Vector3 desired = transform.position - transform.up;
            Debug.Log("Vi un obstaculo");
            return Seek(desired);
        }
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacles))//Punto de inicio, direccion, largo y layermask
        {
            Vector3 desired = transform.position + transform.up;
            Debug.Log("Vi un obstaculo");
            return Seek(desired);
        }
        return Vector3.zero;*/


        if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
            return Seek(transform.position - transform.up);
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
            return Seek(transform.position + transform.up);
        return Vector3.zero;
    }


    private void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, transform.position + transform.right * _viewRadius);

        Vector3 leftRayPos = transform.position + transform.up * 0.5f;
        Vector3 rightRayPos = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.right * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.right * _viewRadius);
    }
}
