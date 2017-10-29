
using System.Collections.Generic;
using UnityEngine;

public class UpdateResult
{
    public List<Action> UpdateActions = new List<Action>();
    public Transition Transition = null;
    public int Level { get; set; }

    public void AddActionToUpdateActions (Action action)
    {
        UpdateActions.Add(action);
    }
}