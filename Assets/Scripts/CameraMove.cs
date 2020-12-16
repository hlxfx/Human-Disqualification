using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class CameraMove 
{
    public Camera camera;
    public Transform target;

    public void Start()
    {
        camera = Camera.main;
        target = RoleInterface.GetPlayer().transform;
    }

    // Update is called once per frame
    public void Update()
    {
        if (target)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(target.position.x, target.position.y, camera.transform.position.z) , Time.deltaTime);
        }
    }

    public bool ChangeTarget(Transform transform)
    {

        return true;
    }
}
