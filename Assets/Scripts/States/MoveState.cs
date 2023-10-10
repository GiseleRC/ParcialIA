using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    Transform _target;
    Renderer _rend;

    public MoveState(Player p)
    {
        _target = p.transform;
        _rend = p.GetComponent<Renderer>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entre a Move");
        _rend.material.color = Color.red;
    }

    public override void OnUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        _target.position += Vector3.right * input * Time.deltaTime;

        if (input == 0)
        {
            fsm.ChangeState(PlayerStates.Idle);
        }
        Debug.Log("Estoy en Move");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Move");
        _rend.material.color = Color.white;
    }
}
