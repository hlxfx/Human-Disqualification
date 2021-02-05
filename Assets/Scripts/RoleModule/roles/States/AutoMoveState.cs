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
    private float offset;
    private Vector3 posOffset;

    public AutoMoveState()
    {
        _state = States.autoMove;
    }

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
            Vector3 dir = path[0] - (ani.gameObject.transform.position + posOffset);
            if(Mathf.Abs(dir.x) < offset && Mathf.Abs(dir.y) < offset)
            {
                path.RemoveAt(0);
                return;
            }

            speedX += dir.x;
            speedY += dir.y;

            if (Mathf.Abs(dir.x) < offset)
            {
                speedX = 0f;
            }
            else
            {
                ani.SetFloat("ValX", (dir.x < 0 ? -1 : 1) * (Mathf.Abs(speedX) > .6f ? .6f : Mathf.Abs(speedX)));
            }
            if (Mathf.Abs(dir.y) < offset)
            {
                speedY = 0f;
            }
            else
            {
                ani.SetFloat("ValY", (dir.y < 0 ? -1 : 1) * (Mathf.Abs(speedY) > .6f ? .6f : Mathf.Abs(speedY)));
            }


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

    public override void OnAutoMove(Animator ani, List<Vector3> dir , float offset)
    {
        path = dir;
        this.offset = offset;
        posOffset = new Vector3(ani.GetComponent<BoxCollider2D>().offset.x, ani.GetComponent<BoxCollider2D>().offset.y, 0f);
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
