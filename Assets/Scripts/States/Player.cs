using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Finite State Machine
    //Estados = Idle, Move, Jump
    //OnEnter(),OnUpdate(),OnExit();

    FiniteStateMachine _fsm; //Con esto lo estamos guardando en nuestra variable

    void Start()
    {
        _fsm = new FiniteStateMachine(); //Esto esta creando una nueva FSM;
        _fsm.AddState(PlayerStates.Idle, new IdleState(this));
        _fsm.AddState(PlayerStates.Move, new MoveState(this));
        //_fsm.AddState(PlayerStates.Dying, new DyingState(this));
        _fsm.ChangeState(PlayerStates.Idle);
    }

    void Update()
    {
        //If(Juego en pausa es falso)
        _fsm.Update();
    }

    public float hp;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0) _fsm.ChangeState(PlayerStates.Dying);
    }
}

public enum PlayerStates
{
    Idle,
    Move,
    Jump,
    Attack,
    Dying
}
