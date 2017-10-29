using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : HSMBase{

   // public List<Transition> Transitions = new List<Transition>();
   // public Action Action { get; set; }
    //public Action EntryAction { get; set; }
   // public Action ExitAction { get; set; }
   // public StateMachine Parent { get; set; }


public static State CreateState(string name, Action entry, Action exit, Action update, StateMachine parent, List<Transition> transitions)
    {
        State newState = new State();
        newState.Name = name;
        newState.EntryAction = entry;
        newState.ExitAction = exit;
        newState.UpdateAction = update;
        newState.Parent = parent;
        newState.Transitions = transitions;

        if (newState.Transitions == null)
            newState.Transitions = new List<Transition>();

        return newState;
    }   

}
