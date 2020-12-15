using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MassageNode rootMassageNode;
    public RoleManager roleManager;
    public MapManager mapManager;

    private void Awake()
    {
        if(instance != null) {
            Destroy(instance);
        }
        instance = this;
        InitGmae();  //无存档时调用
    }

    private void InitGmae()
    {
        rootMassageNode = new MassageNode(gameObject);
        mapManager = new MapManager(GameObject.Find("Canvas/map"));
        roleManager = new RoleManager(GameObject.Find("Canvas/map/role"));
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
