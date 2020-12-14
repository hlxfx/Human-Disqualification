using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MassageNode rootMassageNode;
    public RoleManager roleManager;

    private void Awake()
    {
        if(instance != null) {
            Destroy(instance);
        }
        instance = this;
        rootMassageNode = new MassageNode(gameObject);
        //roleManager = new RoleManager(GameObject.Find("Canvas/Image/role"));
    }
    // Start is called before the first frame update
    void Start()
    {
        roleManager.Start();
    }

    // Update is called once per frame
    void Update()
    {
        roleManager.Update();
    }

    private void FixedUpdate()
    {
    }

    private void OnDestroy()
    {
        
    }
}
