using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.transform.tag == "enterDoor")
		{
			GameManager.instance.rootMassageNode.SendEvent(MassageList.EnterMap, coll.gameObject.name,coll.gameObject);
			return;
		}

		if (coll.transform.tag == "outDoor")
        {
			GameManager.instance.rootMassageNode.SendEvent(MassageList.OutMap, coll.gameObject.name);
			return;
		}
	}
}
