using UnityEngine;

public class SteeringAgentClass4 : MonoBehaviour
{
    //[SerializeField] Transform _seekTarget, _fleeTarget;
    [SerializeField] protected float _maxSpeed, _maxForce;
    [SerializeField] protected float _viewRadius;
    [SerializeField] protected LayerMask _obstacles;

    protected Vector3 _velocity;

    protected void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        if (_velocity != Vector3.zero) transform.right = _velocity;
    }

    protected bool HastToUseObstacleAvoidance()
    {
        Vector3 avoidanceObs = ObstacleAvoidance();
        AddForce(avoidanceObs);
        return avoidanceObs != Vector3.zero;
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxSpeed);
    }

    protected Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = (targetPos - transform.position).normalized * speed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    //GOES TO (Esto dice que FLEE devuelve -Seek(targetPos))
    protected Vector3 Flee(Vector3 targetPos) => -Seek(targetPos);
  

    protected Vector3 Arrive(Vector3 targetPos)
    {
        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist > _viewRadius) return Seek(targetPos);

        return Seek(targetPos, _maxSpeed * (dist / _viewRadius));
    }

    protected Vector3 ObstacleAvoidance()
    {

        if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
            return Seek(transform.position - transform.up);
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
            return Seek(transform.position + transform.up);
        return Vector3.zero;
    }

    protected Vector3 Pursuit(SteeringAgentClass4 targetAgent) //Nosotros queremos ir hacia donde va nuestro objetivo, no hacia donde esta
    {
        Vector3 futurePos = targetAgent.transform.position + targetAgent._velocity;
        Debug.DrawLine(transform.position, futurePos, Color.cyan);
        return Seek(futurePos);
    }


    /*protected Vector3 Pursuit(Transform targetPos)
    {
        //return Seek(targetPos + targetPos.forward);
    }

    protected Vector3 Pursuit(Rigidbody targetRB)
    {
        return Seek(targetRB.transform.position + targetRB.velocity);
    }*/



    protected Vector3 Evade(SteeringAgentClass4 targetAgent) //Nosotros queremos ir hacia la direccion contraria donde va nuestro objetivo
    {
        return -Pursuit(targetAgent);
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }


    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Gizmos.color = Color.green;

        Vector3 leftRayPos = transform.position + transform.up * 0.5f;
        Vector3 rightRayPos = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.right * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.right * _viewRadius);
    }
}
