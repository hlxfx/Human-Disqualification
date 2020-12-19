using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum States
{
    move,
    attack,
    injured,
    die,
}

[Serializable]
public class PlayerState :IScript
{
   
    [SerializeField]
    private Animator ani;
    private GameObject gameObject;
    [SerializeField]
    private States _state;
    public State curState;

    [SerializeField]
    private MoveState moveState;
    private AttackState attackState;
    private DieState dieState;
    private InjuredState injuredState;


    public PlayerState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        moveState = new MoveState();
        attackState = new AttackState();
        dieState = new DieState();
        injuredState = new InjuredState();
        SetState(States.move);
        curState = moveState;
    }

    public void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    public void Update() {
        if (!gameObject.activeSelf)
        {
            OnDisEnable();
        }
        else if (ani)
        {
            curState.Update(ani);
        }
    }

    public bool SetState(States state)
    {
        _state = state;
        switch (_state)
        {
            case States.move:
                curState = moveState;
                break;
            case States.attack:
                curState = attackState;
                break;
            case States.die:
                curState = dieState;
                break;
            case States.injured:
                curState = injuredState;
                break;
        }
        return true;
    }

    public States GetState(States state)
    {
        return _state;
    }

    private void OnDisEnable()
    {
        if (gameObject.activeSelf)
        {

        }
    }
}
