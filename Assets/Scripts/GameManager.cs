using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;

    public List<SteeringAgent> allAgents = new List<SteeringAgent>();
    public List<Reward> allRewards = new List<Reward>();
    public List<Obstacle> allObstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(_boundWidth, 0f, _boundHeight));
    }

    public Vector3 AdjustPositionToBounds(Vector3 position)
    {
        float height = _boundHeight / 2;
        float width = _boundWidth / 2;

        if (position.z > height) position.z = -height;
        if (position.z < -height) position.z = height;

        if (position.x > width) position.x = -width;
        if (position.x < -width) position.x = width;

        return position;
    }

}
