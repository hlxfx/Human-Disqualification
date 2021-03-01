using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MassageNode rootMassageNode;
    public RoleManager roleManager;
    public MapManager mapManager;
    public UIManager uIManager;
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
        cameraMove = new CameraMove();
        rootMassageNode = new MassageNode(gameObject);
        mapManager = new MapManager(GameObject.Find("scene/map"));
        roleManager = new RoleManager(GameObject.Find("scene/map/role"));
        uIManager = new UIManager(GameObject.Find("ui"));
    }

    // Start is called before the first frame update
    void Start()
    {
        roleManager.Start();
        cameraMove.Start();
        uIManager.Start();
    }

    // Update is called once per frame
    void Update()
    {
        roleManager.Update();
        cameraMove.Update();
        uIManager.Update();
    }

    private void FixedUpdate()
    {
    }

    private void OnDestroy()
    {
        
    }
}
