using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAction : Action
{
    private string _action;
    public TransitionAction(Transition thisTrans, string action)
    {
        _action = action;
        Debug.Log("here");
        thisTrans.IsTriggered = false;
    }
    public override void Execute()
    {
        Debug.Log(_action);
    }
}
