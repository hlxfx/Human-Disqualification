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
    [SerializeField]
    private Animator ani;
    private GameObject gameObject;
    [SerializeField]
    private State _state;
    [SerializeField]
    private float deltaSpeed = .8f;    //每秒速度的变化量


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
            if (!ani.GetBool("IsMove"))  //停止后重新进入先置零方向
            {
                ani.SetFloat("ValX", 0);
                ani.SetFloat("ValY", 0);
            }
            ani.SetBool("IsMove", true);
            ani.SetFloat("ValX", h * deltaSpeed, .5f, Time.deltaTime);
            ani.SetFloat("ValY", v * deltaSpeed, .5f, Time.deltaTime);
            MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0));
        }
        else
        {
            ani.SetBool("IsMove", false);
        }
    }

    private void MovePos(Vector3 dir)
    {
        if(Math.Abs(dir.x) > .6f || Math.Abs(dir.y) > .6f)
        {
            //后续增加奔跑计时，  加上跑步冷却
            dir.x = Math.Min(dir.x, .6f);
            dir.y = Math.Min(dir.y, .6f);
        }
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
