using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition {

    public bool IsTriggered { get; set; }
    public HSMBase TargetState { get; set; }
    public Action Action { get; set; }
    public string Name { get; set; }

    public int OriginLevel { get; set; }
	
    public int GetLevelDifference( int targetLevel)
    {
        //Debug.Log("GetLevelDef: " + OriginLevel + " " + targetLevel);
        return OriginLevel - targetLevel;
    }

    public static Transition CreateTransition(string name, State target, Action action)
    {
        Transition newTransition = new Transition();
        newTransition.Name = name;
        newTransition.TargetState = target;
        newTransition.Action = action;

        return newTransition;
    }
}
