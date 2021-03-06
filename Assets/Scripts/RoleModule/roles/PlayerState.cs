﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerState :IScript
{
   
    [SerializeField]
    private Animator ani;
    private GameObject gameObject;

    [SerializeField]
    public State curState;

    [SerializeField]
    private MoveState moveState;
    private AttackState attackState;
    private DieState dieState;
    private InjuredState injuredState;
    private AutoMoveState autoMove;


    public PlayerState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        moveState = new MoveState();
        attackState = new AttackState();
        dieState = new DieState();
        injuredState = new InjuredState();
        autoMove = new AutoMoveState();
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
        switch (state)
        {
            case States.move:
                curState = moveState;
                break;
            case States.autoMove:
                curState = autoMove;
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

    public States GetState()
    {
        return curState._state;
    }

    private void OnDisEnable()
    {
        if (gameObject.activeSelf)
        {

        }
    }
}
