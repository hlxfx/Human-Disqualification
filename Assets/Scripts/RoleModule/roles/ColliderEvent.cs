using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.transform.tag == "door")
        {
			GameManager.instance.rootMassageNode.SendEvent(MassageList.ChangeMap, coll.gameObject.name);
		}
	}
}
