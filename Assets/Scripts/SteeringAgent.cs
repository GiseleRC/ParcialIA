using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    [SerializeField] protected float _maxSpeed, _maxForce, _multiplyDir;
    [SerializeField] protected float _viewRadius;
    [SerializeField] protected LayerMask _obstaclesMask;

    protected Vector3 _velocity;

    // ------------------------------------------------- Comportamientos del SteeringAgent (boids y hunter) ---------------------------------------------
    protected void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        if (_velocity != Vector3.zero)
        {
            transform.forward = _velocity;
        }
    }

    //Metodo para evadir los obstaculos
    protected Vector3 Avoidance()
    {
        if (Physics.Raycast(transform.position + transform.right * _multiplyDir, transform.forward, _viewRadius, _obstaclesMask))
        {
            return Seek(transform.position - transform.right);
        }
        else if (Physics.Raycast(transform.position - transform.right * _multiplyDir, transform.forward, _viewRadius, _obstaclesMask))
        {
            return Seek(transform.position + transform.right);
        }
        return Vector3.zero;
    }

    //Metodo para evaluar si usar avoidance, realizando un ajuste en la magnitud del vector con el metodo addforce
    protected bool UseAvoidance()
    {
        Vector3 avoidanceDir = Avoidance();
        AddForce(avoidanceDir);
        return avoidanceDir != Vector3.zero;
    }

    //Metodo para realizar la busqueda de la recompensa, recibiendo un vector director, devolviendo un vector y velocidad maxima del agente, utilizado para el metodo avoidance y el flee para huir
    protected Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxSpeed);
    }

    //Sobrecarga del metodo SEEK, pero este ademas de recibir un vector, recibe una velocidad devolviendo una vector que utilizará la otra sobrecarga de seek
    protected Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = (targetPos - transform.position).normalized * speed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    //Para huir utiliza el mismo vector que para ir a buscar la reocmpensa pero en el sentido contrario para huir
    protected Vector3 Flee(Vector3 targetPos) => -Seek(targetPos);

    //recibe el vector director de la recompensa, calculamos la distancia qu ehay entre nunestro transform y la reward
    protected Vector3 Arrive(Vector3 targetPos)
    {
        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist > _viewRadius) return Seek(targetPos);

        return Seek(targetPos, _maxSpeed * (dist / _viewRadius));
    }

    //recibe un gameobject que posea el script steering agent, para que el npc cazador persiga al boid agent, calculamos la distancia que hay entre entre los agentes,
    //para devolver unn vector para perseguir y ejecutar el seek hacia la posicion calculada
    protected Vector3 Pursuit(SteeringAgent targetSteeringAgent)
    {
        Vector3 commingPos = targetSteeringAgent.transform.position + targetSteeringAgent._velocity;
        Debug.DrawLine(transform.position, commingPos, Color.cyan);
        return Seek(commingPos);
    }

    //Metodo que va a ser utilizado para evadir al hunter, tomando el metodo pursuit a la inversa para huir
    protected Vector3 Evade(SteeringAgent targetAgent)
    {
        return -Pursuit(targetAgent);
    }

    //Controla la distancia de separacion entre agentes en la escena
    protected Vector3 Separation(List<SteeringAgent> agentsInScene)
    {
        Vector3 desiredDir = Vector3.zero;

        foreach (var agent in agentsInScene)
        {
            if (agent == this) continue;

            Vector3 distanceBtAgents = agent.transform.position - transform.position;

            if (distanceBtAgents.sqrMagnitude > _viewRadius * _viewRadius) continue;

            desiredDir += distanceBtAgents;
        }

        if (desiredDir == Vector3.zero) return Vector3.zero;

        desiredDir *= -1;
        return Vector3.ClampMagnitude((desiredDir.normalized * _maxSpeed) - _velocity, _maxForce * Time.deltaTime);
    }

    //Controla  el comportamiento en colmena
    protected Vector3 Cohesion(List<SteeringAgent> agentsInScene)
    {
        Vector3 desiredDir = Vector3.zero;
        int agentsInSceneCount = 0;

        foreach (var agent in agentsInScene)
        {
            if (agent == this) continue;

            Vector3 distance = agent.transform.position - transform.position;

            if (distance.sqrMagnitude > _viewRadius * _viewRadius) continue;

            desiredDir += agent.transform.position;
            agentsInSceneCount++;
        }

        if (agentsInSceneCount == 0) return Vector3.zero; //Si no hay agentes

        desiredDir /= agentsInSceneCount;

        return Seek(desiredDir);
    }

    //formacion y alineamiento de los agentes en escena
    protected Vector3 AgentsAlignment(List<SteeringAgent> boidsInScene)
    {
        Vector3 desiredDir = Vector3.zero;
        int boidsInScenecount = 0;

        foreach (var agent in boidsInScene)
        {
            if (Vector3.Distance(agent.transform.position, transform.position) > _viewRadius) continue;
            desiredDir += agent._velocity;
            boidsInScenecount++;
        }

        desiredDir /= boidsInScenecount;

        return Vector3.ClampMagnitude(desiredDir - _velocity, _maxForce * Time.deltaTime);
    }

    //ajuste de la magnitud del vector recibido
    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    //Metodo para resetear el vector del agent
    public void ResetVector()
    {
        transform.position = Vector3.zero;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Gizmos.color = Color.green;

        Vector3 leftRayPos = transform.position + transform.right * _multiplyDir;
        Vector3 rightRayPos = transform.position - transform.right * _multiplyDir;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.forward * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.forward * _viewRadius);
    }
}
