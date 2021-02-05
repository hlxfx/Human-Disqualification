using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredState : State
{
  
    public InjuredState()
    {
        _state = States.injured;
    }

    // Update is called once per frame
    public override void Update(Animator ani)
    {
        
    }
}
