using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMiner
{
    StateMachine topLevelSM;
    State alert = new State();
    State search = new State();
    State mine = new State();
    State move = new State();
    StateMachine workSM = new StateMachine();

    //set up transitions 
    Transition fromWorkToAlert = new Transition();
    Transition fromSearchToMove = new Transition();
    Transition fromMoveToSearch = new Transition();
    Transition fromMoveToMine = new Transition();
    Transition fromMineToMove = new Transition();

    private float _timer = 0;
    private UpdateResult _result;


    public void Go()
    {

        fromMineToMove.Name = "fromMineToMove";
        fromSearchToMove.Name = "fromSearchToMove";
        fromMoveToMine.Name = "fromMoveToMine";
        fromMoveToSearch.Name = "fromMoveToSearch";
        fromWorkToAlert.Name = "fromWorkToAlert";




        Action topLevelAction = new PrintAction("");
        topLevelSM = new StateMachine();
        topLevelSM.UpdateAction = topLevelAction;

        //set up children actions



        topLevelSM.AddChildToList(alert);
        topLevelSM.AddChildToList(workSM);

        workSM.AddChildToList(search);
        workSM.AddChildToList(mine);
        workSM.AddChildToList(move);
        workSM.InitialState = search;


        topLevelSM.InitialState = search;
        topLevelSM.CurrentState = topLevelSM.InitialState;



        search.Transitions.Add(fromSearchToMove);
        move.Transitions.Add(fromMoveToMine);
        move.Transitions.Add(fromMoveToSearch);
        mine.Transitions.Add(fromMineToMove);
        mine.Transitions.Add(fromMoveToMine);
        workSM.Transitions.Add(fromWorkToAlert);



        search.UpdateAction = new PrintAction("Searching....");
        move.UpdateAction = new PrintAction("Mine found moving there.....");
        mine.UpdateAction = new PrintAction("Mining Gold.......");
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.M))
        {
            fromSearchToMove.Action = new TransitionAction(fromSearchToMove, "fromSearchToMove trans action");
            fromSearchToMove.OriginLevel = 0;
            fromSearchToMove.TargetState = move;
            fromSearchToMove.IsTriggered = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            fromMoveToMine.Action = new TransitionAction(fromMoveToMine, "fromMoveToMine trans action");
            fromMoveToMine.OriginLevel = 0;
            fromMoveToMine.TargetState = mine;
            fromMoveToMine.IsTriggered = true;
        }
        _result = topLevelSM.Update();

        if (_timer > 1)
        {
            foreach (var action in _result.UpdateActions)
            {
                if (action != null)
                    action.Execute() ;
                Debug.Log(topLevelSM.CurrentState);
            }
            _timer = 0;
        }

        _result = null;
    }

}
