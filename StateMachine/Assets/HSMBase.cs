using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HSMBase 
{
    public List<Transition> Transitions = new List<Transition>();
    public Action EntryAction { get; set; }
    public Action ExitAction { get; set; }
    public Action UpdateAction { get; set; }
    public StateMachine Parent { get; set; }
    public string Name { get; set; }

    public virtual UpdateResult Update()
    {
        UpdateResult _result = new UpdateResult();
       
        _result.UpdateActions.Add(UpdateAction);
        _result.Transition = null;
        _result.Level = 0;

        return _result;
    }

   
}
