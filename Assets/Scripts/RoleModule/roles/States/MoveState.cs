using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MoveState : State
{
    public const float maxRunTime = 5f;
    [SerializeField]
    private float deltaSpeed = .6f;    //每秒速度的变化量及步行时最大可达速度
    public bool canRun = true;
    public float runTime = maxRunTime;
    public float runRestTime = 3f;
    public float runSpeed = 2.5f;      //为普通速度的多少倍

    // Update is called once per frame
    public override void Update(Animator ani)
    {
        OnMove(ani);
    }

    private void OnMove(Animator ani)
    {
        float h = GameInput.GetAxis("Horizontal");
        float v = GameInput.GetAxis("Vertical");
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

            if (GameInput.GetKey(KeyCode.LeftShift) && canRun)
            {
                Run(ani);
            }
            else
            {
                if (!canRun || runTime < maxRunTime)
                {
                    rest();
                }
                MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0) , ani.gameObject);
            }
        }
        else
        {
            rest();
            ani.SetBool("IsMove", false);
        }
    }

    private void Run(Animator ani)
    {
        MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0) * runSpeed, ani.gameObject);
        runTime -= Time.deltaTime;
        if (runTime < 0)
        {
            canRun = false;
        }
    }

    private void rest()
    {
        runTime += maxRunTime / runRestTime * Time.deltaTime;
        if (runTime > maxRunTime)
        {
            canRun = true;
            runTime = maxRunTime;
        }
    }

    private void MovePos(Vector3 dir,GameObject gameObject)
    {
        gameObject.transform.position += dir * Time.deltaTime;
    }
}
