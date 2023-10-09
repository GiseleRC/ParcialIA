using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _radius;
    [SerializeField] Transform _cube;

    void Start()
    {
        var a = new Vector2(2, 1);

        var magnitude1 = Mathf.Sqrt((a.x * a.x) + (a.y * a.y));

        var magnitude2 = a.magnitude;

        Debug.Log(magnitude2);

        float magnitude3 = GetMagnitude(a);

        var magnitude4 = a.sqrMagnitude;

        Debug.Log(magnitude4);

        var aEjercicio = new Vector2(1, 0);
        aEjercicio *= 4;

        aEjercicio = (aEjercicio / aEjercicio.magnitude) * 2.5f;
        aEjercicio = aEjercicio.normalized * 2.5f;


    }

    private float GetMagnitude(Vector2 a)
    {
        return Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, _radius);

        Vector2 a = new Vector2(4, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, a);

        Vector2 b = new Vector2(4, 3);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, b);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, _cube.position);

        Gizmos.color = new Color(1, 0.7526976f, 0f); //Orange
        Gizmos.DrawLine(Vector3.zero, Vector3.ClampMagnitude(_cube.position, 3));

        //Vector2 c = _cube.position;
        //if (c.magnitude > 3) c = c.normalized * 3;
        //Gizmos.DrawLine(Vector3.zero, c);

        //int hp = 10;
        //int maxHp = 15;
       // int potion = 20;

        //hp += potion;
        //if (hp > maxHp) hp = maxHp;
        //if (hp < 0) hp = 0;

        //hp = Mathf.Clamp(hp + potion, 0, maxHp);
    }
}
