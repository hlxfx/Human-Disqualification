using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test99 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnDrawGizmosSelected()
    {
        SpriteRenderer[] temp = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.layer == 9)
                Gizmos.color = color;
            else
                Gizmos.color = Color.clear;
            Gizmos.DrawWireCube(temp[i].bounds.center, new Vector3(0.16f, 0.16f, 0));
        }
    }
}

