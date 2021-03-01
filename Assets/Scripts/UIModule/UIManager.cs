using System.Collections;
using UnityEngine;
using System;

public class UIManager : IScript
{
    private BagManager bagManager;


    private RaycastHit hit;
    private Ray ray;

    public UIManager(GameObject ui)
    {
        bagManager = new BagManager(ui.transform.Find("bag"));
    }

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (GameInput.GetKeyDown(KeyCode.B))
        {
            bagManager.OpenBag();
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("UI")))
        {
            GameObject obj = hit.collider.gameObject;
            obj.SendMessage("OnClick", hit.point, SendMessageOptions.DontRequireReceiver);




        }
    }
}
