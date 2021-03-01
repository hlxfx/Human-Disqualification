using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager
{
    public bool isOpen = false;
    private List<GameObject> slots;
    private int maxSlotNum = 40;
    private GameObject bag;

    public BagManager(Transform bag)
    {
        this.bag = bag.gameObject;
        //获取slot的宽高 并用于计算slot在bag中的位置
        GameObject prefab = GeneraMethod.LoadGameObject("prefabs/bag/slot");
        float width = prefab.GetComponent<RectTransform>().rect.width;
        float offsetX = width / 2 + 5;
        float height = prefab.GetComponent<RectTransform>().rect.height;
        float offsetY = height / 2 + 5 + 10; //加上与顶格的间距

        Transform rootSlot = bag.Find("Viewport/slots");
        slots = new List<GameObject>();
        for(int i = 0; i < maxSlotNum; i++)
        {
            GameObject slot = GeneraMethod.LoadGameObject("prefabs/bag/slot");
            slot.transform.SetParent(rootSlot);
            slot.transform.localPosition = new Vector3(offsetX + i % 4 * (width + 5), -offsetY - i / 4 * (height + 5), 0);
            slot.name = "slot" + i;
            slots.Add(slot);
        }
    }

    public void OpenBag()
    {
        if (isOpen)
        { 
            CloseBag();
            return;
        }

        bag.SetActive(true);
        isOpen = true;
    }

    public void CloseBag()
    {
        bag.SetActive(false);
        isOpen = false;
    }

    public GameObject FindEmpty()
    {
        foreach (var item in slots)
        {
            if(item.transform.childCount < 1)
            {
                return item;
            }
        }

        return null;
    }


}
