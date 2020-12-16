using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MassageNode rootMassageNode;
    public RoleManager roleManager;
    public MapManager mapManager;
    public CameraMove cameraMove;

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
        cameraMove = new CameraMove();
    }

    // Start is called before the first frame update
    void Start()
    {
        roleManager.Start();
        cameraMove.Start();
    }

    // Update is called once per frame
    void Update()
    {
        roleManager.Update();
        cameraMove.Update();
    }

    private void FixedUpdate()
    {
    }

    private void OnDestroy()
    {
        
    }
}
