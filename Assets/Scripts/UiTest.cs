using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTest : MonoBehaviour , IMassageInterface
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HandleMessage(int id, object param1, object param2)
    {
        return false;
    }


    public int MessagePriority() {

        return 1;
    }
}
