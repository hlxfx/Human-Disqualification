using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneraMethod
{
    public static GameObject LoadGameObject(string path)
    {
        GameObject temp = Resources.Load<GameObject>(path);
        temp = GameObject.Instantiate(temp);
        return temp;
    }

    public static Sprite LoadSprite(string path)
    {
        Sprite temp = Resources.Load<Sprite>(path);
        return temp;
    }

    public static TextAsset LoadTextAsset(string path)
    {
        TextAsset temp = Resources.Load<TextAsset>(path);
        return temp;
    }
}
    