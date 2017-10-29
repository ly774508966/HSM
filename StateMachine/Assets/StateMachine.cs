using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : HSMBase
{

    public List<HSMBase> ChildStates = new List<HSMBase>();
    public HSMBase InitialState;
    public HSMBase CurrentState;
    public HSMBase TargetState;
    //  public Action Action;
   // public StateMachine Parent;
    //public int Level;

    private UpdateResult _result = new UpdateResult();

    // Update is called once per frame
    public override UpdateResult Update()
    {
        _result.UpdateActions.Clear();

        //The state machine asks the current state to return its hierarchy
        //if it is a terminal state then it returns itself otherwise it returns itself and adds to it the hierarchy of states from its own current state
        if (CurrentState == null)
        {
            CurrentState = InitialState;
            if (CurrentState.EntryAction != null)
            {
                _result.AddActionToUpdateActions(CurrentState.EntryAction);

                // _result.UpdateActions.Add(CurrentState.EntryAction);
            }

            return _result;
        }

        Debug.Log("CurrentState: " + CurrentState.Name);
        //try to find a transition in the current state
        Transition triggeredTransition = null;
        foreach (var transition in CurrentState.Transitions)
        {
            if (transition.IsTriggered)
            {
                //Debug.Log(transition.Name + " is triggered");
                triggeredTransition = transition;
                transition.IsTriggered = false;
                break;
            }
        }
        //if we found a triggered transition make a result struct for it
        if (triggeredTransition != null)
        {
            if (triggeredTransition.Action != null)
            {
                //  Debug.Log("Transition has an action : " + triggeredTransition.Action);
                //  _result.UpdateActions.Add(triggeredTransition.Action);
                //   Debug.Log("Add");
                _result.AddActionToUpdateActions(triggeredTransition.Action);
            }

            _result.Transition = triggeredTransition;
            _result.Level = triggeredTransition.GetLevelDifference(_result.Level);
        }
        //if no transition is found recurse down until we get a result
        else
        {
            // Debug.Log("No transitions from " + this + " checking current state " + CurrentState);
            // here the current state will check all of its states for a transition
            _result = CurrentState.Update();// here the CurrentState becomes the topLevel state and runs its update method looking for transitions
        }





        /////////////////////////////////////////////////check if we have a transition/////////////////////////////////////////////////////////////

        if (_result.Transition == null)
        {
            //Debug.Log("No transitions adding current state action");
            //no transition we can just do our normal action
            if (UpdateAction != null)
            {
                //_result.UpdateActions.Add(CurrentStateAction);
                //Debug.Log("Add");
                _result.AddActionToUpdateActions(UpdateAction);
            }

        }


        ////////////////////////////////////////////////////////////////////////////////////


        //we have a transition
        else if (_result.Transition != null)
        {
              // Debug.Log("There is a transition, checking level now");
            //act based on its level
            if (_result.Level == 0)
            {
                 Debug.Log("Level == 0");
                //honor immediatly 
                TargetState = _result.Transition.TargetState;

                //_result.UpdateActions.Add(CurrentState.ExitAction);
                //_result.UpdateActions.Add(_result.Transition.Action);
                //_result.UpdateActions.Add(TargetState.EntryAction);

                //   Debug.Log("Add");
       

                if (CurrentState.ExitAction != null)
                    _result.AddActionToUpdateActions(CurrentState.ExitAction);

                if (_result.Transition.Action != null)
                    _result.AddActionToUpdateActions(_result.Transition.Action);

                if (TargetState.EntryAction != null)
                    _result.AddActionToUpdateActions(TargetState.EntryAction);


                CurrentState = TargetState;

                //add our normal action(we may be a state)????????
                //_result.Actions.Add(Action);

                //Clear the transition, so its not fired again??
                _result.Transition = null;
            }
            else if (_result.Level > 0)
            {
                   Debug.Log("Level > 0");
                //its destined for a higher level?????
                //exit our current state??
                if (CurrentState.ExitAction != null)           
                    _result.AddActionToUpdateActions(CurrentState.ExitAction);
                

                CurrentState = null;

                //decrease level and start over??
                _result.Level--;
            }
            else if (_result.Level < 0)
            {
                  Debug.Log("Level < 0");
                // we need to pass the update down??
                TargetState = _result.Transition.TargetState;
                var targetStateMachine = (StateMachine)TargetState.Parent;
                //  _result.UpdateActions.Add(_result.Transition.Action);
                //   Debug.Log("Add");
                _result.AddActionToUpdateActions(_result.Transition.Action);

                targetStateMachine.UpdateDown(TargetState, -_result.Level);

                //clear transition
                _result.Transition = null;
            }
        }
        return _result;
    }


    //Recurses up the parent hierarchy, transitioning inot teach satte in turn for the given number of levels
    void UpdateDown(HSMBase state, int level)
    {
         Debug.Log("Calling updateDown");
        //if were not at the toplevel continue recursing
        if (level > 0)
        {
            //pass ourself as the transition state to our parent
            Parent.UpdateDown(this, level--);//?????????????????????????????????????????
        }
        else if (level == 0)
        {
            //do nothing?????      
        }

        //if we have a currentState exit it
        if (CurrentState != null)
        {
            // _result.UpdateActions.Add(CurrentState.ExitAction);
            //   Debug.Log("Add");
            _result.AddActionToUpdateActions(CurrentState.ExitAction);
            //move to the new satte, and return all the actions
            CurrentState = (State)state;
            State newState = (State)state;
            // _result.UpdateActions.Add(newState.EntryAction);
            //   Debug.Log("Add");
            _result.AddActionToUpdateActions(newState.EntryAction);
        }
    }




    public void AddChildToList(HSMBase state)
    {
        ChildStates.Add(state);
        state.Parent = this;
    }

    public static StateMachine CreateStateMachine(string name, Action entry, Action exit, Action update, StateMachine parent, State initial, State current, List<Transition> transitions)
    {
        StateMachine newSM = new StateMachine();
        newSM.Name = name;
        newSM.EntryAction = entry;
        newSM.ExitAction = exit;
        newSM.UpdateAction = update;
        newSM.Parent = parent;
        newSM.InitialState = initial;
        newSM.CurrentState = current;
        newSM.Transitions = transitions;

        if (newSM.Transitions == null)
            newSM.Transitions = new List<Transition>();

        return newSM;
    }
}

