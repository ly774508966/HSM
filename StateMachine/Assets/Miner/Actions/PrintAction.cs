using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAction : Action
{
    private string action;
    public PrintAction(string printString)
    {
        action = printString;
    }
    public override void Execute()
    {
        Debug.Log(action);
    }

}
