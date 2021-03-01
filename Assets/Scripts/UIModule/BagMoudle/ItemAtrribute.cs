using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum quality
{
    normal,

}

public struct MyAttribute
{
    string name;
    string descript;
    quality quality;
}

public class ItemAtrribute
{
    public MyAttribute attribute;
    protected int nums;


    public ItemAtrribute(MyAttribute attribute)
    {
        this.attribute = attribute;
    }

    public int AddNum()
    {
        return ++nums;
    }

    public int ReduceNum()
    {
        return --nums;
    }

}
