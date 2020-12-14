using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State
{
    move,
    attack,
    injured,
    die,
}

[Serializable]
public class PlayerState :IScript
{
    public const float maxRunTime = 5f;

    [SerializeField]
    private Animator ani;
    private GameObject gameObject;
    [SerializeField]
    private State _state;
    [SerializeField]
    private float deltaSpeed = .6f;    //每秒速度的变化量
    public bool canRun = true;
    public float runTime = maxRunTime;
    public float runRestTime = 3f;
    public float runSpeed = 1.5f;      //为普通速度的多少倍


    public PlayerState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        _state = State.move;
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
            switch (_state)
            {
                case State.move:
                    OnMove();
                    break;
                case State.attack:
                    break;
                case State.injured:
                    break;
                case State.die:
                    break;
            }
        }
    }

    private void OnMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            if (!ani.GetBool("IsMove"))  //停止后重新进入先置速度矢量为0
            {
                ani.SetFloat("ValX", 0);
                ani.SetFloat("ValY", 0);
            }

            ani.SetBool("IsMove", true);
            ani.SetFloat("ValX", h * deltaSpeed, .5f, Time.deltaTime);
            ani.SetFloat("ValY", v * deltaSpeed, .5f, Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift) && canRun)
            {
                Run();
            }
            else
            {
                if(!canRun || runTime < maxRunTime)
                {
                    rest();
                }
                MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0));
            }
        }
        else
        {
            rest();
            ani.SetBool("IsMove", false);
        }
    }

    private void Run()
    {
        MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0) * runSpeed);
        runTime -= Time.deltaTime;
        Debug.Log(runTime);

        if (runTime < 0)
        {
            canRun = false;
        }
    }

    private void rest()
    {
        runTime += maxRunTime / runRestTime * Time.deltaTime;
        if(runTime > maxRunTime)
        {
            canRun = true;
            runTime = maxRunTime;
        }
    }

    private void MovePos(Vector3 dir)
    {
        gameObject.transform.position += dir * Time.deltaTime;
    }
    public bool SetState(State state)
    {
        _state = state;
        return true;
    }

    public State GetState(State state)
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
