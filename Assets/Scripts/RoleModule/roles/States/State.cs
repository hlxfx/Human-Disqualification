using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class State 
{
    public virtual void Update(Animator ani) { }

    public virtual void OnAutoMove(Animator ani,List<Vector3> dir,float offset){ }
}
