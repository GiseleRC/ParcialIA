using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    Player _player;

    public IdleState(Player p)
    {
        _player = p;
    }

    public override void OnEnter()
    {
        Debug.Log("Entre a Idle");
    }

    public override void OnUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            fsm.ChangeState(PlayerStates.Move);
        }
        Debug.Log("Estoy en Idle");
    }

    public override void OnExit()
    {
        Debug.Log("Sali de Idle");
    }
}
