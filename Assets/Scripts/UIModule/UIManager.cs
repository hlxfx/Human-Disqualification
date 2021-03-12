using System.Collections;
using UnityEngine;
using System;

public class UIManager : IScript
{
    private BagManager bagManager;



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
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero ,100f ,LayerMask.GetMask("UI"));
        if (hit.collider != null)
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log(obj.name);
        }
    }
}
