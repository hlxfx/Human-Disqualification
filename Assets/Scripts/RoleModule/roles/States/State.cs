using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum States
{
    move,
    autoMove,
    attack,
    injured,
    die,
}

[Serializable]
public class State 
{
    public States _state;
    public virtual void Update(Animator ani) { }

    public virtual void OnAutoMove(Animator ani,List<Vector3> dir,float offset){ }
}
