using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveState : State
{
    public const float maxRunTime = 5f;
    [SerializeField]
    private float deltaSpeed = 3f;    //每秒速度的变化量及步行时最大可达速度
    public float curSpeed = 0f;
    public bool canRun = true;
    public float runTime = maxRunTime;
    public float runRestTime = 3f;
    public float runSpeed = 2.5f;      //为普通速度的多少倍

    public float speedX = 0f;
    public float speedY = 0f;

    private List<Vector3> path;

    public override void Update(Animator ani)
    {
        //curSpeed = 
        float h = GameInput.GetAxis("Horizontal");
        float v = GameInput.GetAxis("Vertical");
        if(h != 0 || v != 0)
        {
            RoleInterface.SetPlayerState(States.move);
        }

        if (path.Count > 0)
        {
            ani.SetBool("IsMove", true);
            Vector3 dir = path[0] - ani.gameObject.transform.position;
            if(Mathf.Abs(dir.x) <0.01f && Mathf.Abs(dir.y) < 0.01f)
            {
                path.RemoveAt(0);
                return;
            }

            speedX += Mathf.Abs(dir.x) < .01f ? 0 : dir.x;
            speedY += Mathf.Abs(dir.y) < .01f ? 0 : dir.y;

            ani.SetFloat("ValX", (Mathf.Abs(dir.x) < .01f ? 0 : dir.x < 0 ? -1 : 1) * (Mathf.Abs(speedX) > .6f ? .6f : Mathf.Abs(speedX)));
            ani.SetFloat("ValY", (Mathf.Abs(dir.y) < .01f ? 0 : dir.y < 0 ? -1 : 1) * (Mathf.Abs(speedY) > .6f ? .6f : Mathf.Abs(speedY)));


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
                MovePos(new Vector3(ani.GetFloat("ValX"), ani.GetFloat("ValY"), 0), ani.gameObject);
            }
        }
        else
        {
            speedX = speedY = 0f;
            ani.SetBool("IsMove", false);
            RoleInterface.SetPlayerState(States.move);
        }
    }

    public override void OnAutoMove(Animator ani, List<Vector3> dir)
    {
        path = dir;
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

    private void MovePos(Vector3 dir, GameObject gameObject)
    {
        gameObject.transform.position += dir * Time.deltaTime;
    }
}
