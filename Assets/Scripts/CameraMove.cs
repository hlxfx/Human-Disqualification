using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class CameraMove 
{
    public Camera camera;
    public Transform target;
    public Transform light;

    private Vector3 mousePos;

    public void Start()
    {
        camera = Camera.main;
        target = RoleInterface.GetPlayer().transform;
        light = target.transform.Find("light");
    }

    // Update is called once per frame
    public void Update()
    {
        if (target)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(target.position.x, target.position.y, camera.transform.position.z) , Time.deltaTime);

            //如果有手电筒，手电筒方向跟随鼠标
            if (light.gameObject.activeSelf && GameInput.GetMouseBtn(0))
            {
                mousePos = camera.ScreenToWorldPoint(GameInput.GetMousePos());
                Vector3 dir = (mousePos - camera.transform.position).normalized;
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                light.eulerAngles = new Vector3(0, 0, angle - 90);
            }
        }
    }

    /// <summary>
    /// 不插值，直接移动摄像机， 用于场景切换等
    /// </summary>
    public void SetCameraPos(Vector3 pos)
    {
        if(camera != null)
        {
            camera.transform.position = new Vector3(target.position.x, target.position.y, camera.transform.position.z);
        }
    }

    public bool ChangeTarget(Transform transform)
    {

        return true;
    }
}
