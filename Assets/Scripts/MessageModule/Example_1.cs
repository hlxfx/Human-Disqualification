using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_1 : MonoBehaviour, IMassageInterface
{

    private void Awake()
    {
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
