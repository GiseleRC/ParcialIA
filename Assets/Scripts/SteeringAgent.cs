using System.Collections.Generic;
using UnityEngine;

//Clase madre
public class SteeringAgent : MonoBehaviour
{
    protected Vector3 _velocity;

    [SerializeField] protected float _maxSpeed, _maxForce, _multiplyDir;
    [SerializeField] protected float _viewRadius;
    [SerializeField] protected LayerMask _obstaclesMask;

    protected void Movement()
    {
        transform.position += _velocity * Time.deltaTime;
        if (_velocity != Vector3.zero)
        {
            transform.right = _velocity;
        }
    }

    //Metodo para evadir los obstaculos
    protected Vector3 Avoidance()
    {
        if (Physics.Raycast(transform.position + transform.up * _multiplyDir, transform.right, _viewRadius, _obstaclesMask))
        {
            return Seek(transform.position - transform.up);
        }
        else if (Physics.Raycast(transform.position - transform.up * _multiplyDir, transform.right, _viewRadius, _obstaclesMask))
        {
            return Seek(transform.position + transform.up);
        }

        return Vector3.zero;
    }

    //Metodo para evaluar si usar avoidance
    protected bool UseAvoidance()
    {
        Vector3 avoidanceDir = Avoidance();
        AddForce(avoidanceDir);
        return avoidanceDir != Vector3.zero;
    }

    //Metodo para realizar la busqueda de la recompensa, recibiendo un vector director, devolviendo un vector y velocidad maxima del agente, utilizado para el metodo avoidance y el flee para huir
    protected Vector3 Seek(Vector3 rewardDir)
    {
        return Seek(rewardDir, _maxSpeed);
    }

    //Sobrecarga del metodo SEEK, pero este ademas de recibir un vector, recibe una velocidad devolviendo una vector que utilizará la otra sobrecarga de seek
    protected Vector3 Seek(Vector3 rewardDir, float speed)
    {
        Vector3 desired = (rewardDir - transform.position).normalized * speed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    //Para huir utiliza el mismo vector que para ir a buscar la reocmpensa pero en el sentido contrario para huir
    protected Vector3 Flee(Vector3 rewardDir) => -Seek(rewardDir);

    //recibe el vector director de la recompensa, calculamos la distancia qu ehay entre nunestro transform y la reward
    protected Vector3 Arrive(Vector3 rewardDir)
    {
        float distanceOfReward = Vector3.Distance(transform.position, rewardDir);
        if (distanceOfReward > _viewRadius) return Seek(rewardDir);

        return Seek(rewardDir, _maxSpeed * (distanceOfReward / _viewRadius));
    }

    //recibe el vector director de la recompensa, calculamos la distancia qu ehay entre nunestro transform y la reward
    protected Vector3 Pursuit(SteeringAgent targetAgent)
    {
        Vector3 futurePos = targetAgent.transform.position + targetAgent._velocity;
        Debug.DrawLine(transform.position, futurePos, Color.cyan);
        return Seek(futurePos);
    }

    protected Vector3 Evade(SteeringAgent targetAgent)
    {
        return -Pursuit(targetAgent);
    }

    public void ResetPos()
    {
        transform.position = Vector3.zero;
    }

    protected Vector3 Alignment(List<SteeringAgent> agents)
    {
        Vector3 desired = Vector3.zero;
        int boidsCount = 0;

        foreach (var item in agents)
        {
            if (Vector3.Distance(item.transform.position, transform.position) > _viewRadius) continue;

            //Promedio = Suma / Cantidad
            //Matematica  = 7, 9, 8
            //Promedio = 24/3 = 8;

            desired += item._velocity;
            boidsCount++;
        }

        desired /= boidsCount;

        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    protected Vector3 Separation(List<SteeringAgent> agents)
    {
        Vector3 desired = Vector3.zero;

        foreach (var item in agents)
        {
            if (item == this) continue; //Ignorar mi propio calculo

            Vector3 dist = item.transform.position - transform.position;

            if (dist.sqrMagnitude > _viewRadius * _viewRadius) continue;

            desired += dist;
        }

        if (desired == Vector3.zero) return Vector3.zero;
        desired *= -1;
        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    protected Vector3 Cohesion(List<SteeringAgent> agents)
    {
        Vector3 desired = Vector3.zero;
        int boidsCount = 0;

        foreach (var item in agents)
        {
            if (item == this) continue; //Ignorar mi propio calculo

            Vector3 dist = item.transform.position - transform.position;

            if (dist.sqrMagnitude > _viewRadius * _viewRadius) continue;

            //Promedio = Suma / Cantidad
            desired += item.transform.position;
            boidsCount++;
        }

        if (boidsCount == 0) return Vector3.zero; //Si no hay agentes

        desired /= boidsCount;

        return Seek(desired);
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired - _velocity, _maxForce * Time.deltaTime);
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
