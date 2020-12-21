using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Example_1 : MonoBehaviour, IMassageInterface
{
    test test;
    private void Awake()
    {
        test = new test();
        GameManager.instance.rootMassageNode.AttachEventListener(666, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.rootMassageNode.SendEvent(MassageList.example_1);
        GameManager.instance.rootMassageNode.SendEvent(666);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HandleMessage(int id, string param1, GameObject param2)
    {

        if(id == 666)
        {
            Debug.Log("你cai是大傻吊!");
        }
        else
        {
            Debug.Log("你是大傻吊!");
        }
        return false;
    }


    public int MessagePriority()
    {

        return 1;
    }
}

public class test
{
    public test() {
        init(test1);
    }

    public void init(Action test)
    {
        test?.Invoke();
    }

    public void test1()
    {
        Debug.Log("ssss");
    }
}
