using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMiner2
{



    StateMachine topLevelSM;

    StateMachine workSM;
   
    State search;
    State move;
    State mine;
    State alert;

    Transition fromTopToSearch;
    Transition fromSearchToMove;
    Transition fromMoveToMine;
    Transition fromMineToMove;
    Transition fromMoveToSearch;
    Transition fromWorkToAlert;
    Transition fromAlertToWork;
    Transition fromAlertToMine;
    private float _timer = 0f;
    private UpdateResult _result = new UpdateResult();
    public void Go()
    {
        

        fromWorkToAlert = Transition.CreateTransition("fromWorkToAlert", null, new PrintAction("fromWorkToAlert"));
        fromAlertToWork = Transition.CreateTransition("fromAlertToWork", null, new PrintAction("fromAlertToWork"));
        
        fromAlertToMine = Transition.CreateTransition("fromAlertToMine", null, new PrintAction("fromAlertToMine"));

        fromTopToSearch = Transition.CreateTransition("fromTopToSearch", null, new PrintAction("fromTopToSearchAction"));
        fromSearchToMove = Transition.CreateTransition("fromSearchToMove", null, new PrintAction("fromSearchToMoveAction"));
        fromMoveToMine = Transition.CreateTransition("fromMoveToMine", null, new PrintAction("fromMoveToMineAction"));
        fromMineToMove = Transition.CreateTransition("fromMoneToMove", null, new PrintAction("fromMineToMove"));
        fromMoveToSearch = Transition.CreateTransition("fromMoveToSearch", null, new PrintAction("fromMoveToSearch"));


        topLevelSM =StateMachine.CreateStateMachine("TopeLevelSM", new PrintAction("TopLevelEntryAction"),
        new PrintAction("TopeLevelExitAction"), new PrintAction("TopeLevelUpdateAction"), null, null, null,
         new List<Transition>() { fromTopToSearch });
        workSM = StateMachine.CreateStateMachine("WorkSM", new PrintAction("WorkEntryAction"),
        new PrintAction("WorkExitAction"), new PrintAction("WorkAction"), topLevelSM,null, null, new List<Transition>() { fromWorkToAlert });

        search = State.CreateState("Search", new PrintAction("SearchEntryAction"), new PrintAction("SearchExitAction"),
             new PrintAction("SearchUpdateAction"), workSM, new List<Transition>() { fromSearchToMove });
        move = State.CreateState("Move", new PrintAction("MoveEntryAction"), new PrintAction("MoveExitAction"), new PrintAction("MoveUpdateAction"), workSM,
         new List<Transition>() { fromMoveToMine, fromMoveToSearch });
        mine = State.CreateState("Mine", new PrintAction("MineEntryAction"), new PrintAction("MineExitAction"), new PrintAction("MineUpdateAction"), workSM,
            new List<Transition>() { fromMineToMove }); 
        alert = State.CreateState("Alert", new PrintAction("AlertEntryAction"), new PrintAction("AlertExitAction"), new PrintAction("AlertUpdateAction"), workSM,
            new List<Transition>() { fromAlertToWork, fromAlertToMine });



        fromSearchToMove.TargetState = move;
        fromTopToSearch.TargetState = search;
        fromMoveToMine.TargetState = mine;
        fromMineToMove.TargetState = move;
        fromMoveToSearch.TargetState = search;
        fromWorkToAlert.TargetState = alert;
        fromAlertToWork.TargetState = workSM;
        fromAlertToMine.TargetState = mine;

        topLevelSM.InitialState = workSM;
       

        workSM.InitialState = search;
   

    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.M))
        {
            //maybe register for call back to turn this false
            fromSearchToMove.IsTriggered = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            fromMoveToMine.IsTriggered = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            fromMineToMove.IsTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            fromMoveToSearch.IsTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            fromWorkToAlert.IsTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            fromAlertToWork.IsTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            fromAlertToMine.IsTriggered = true;
        }

        _result = topLevelSM.Update();

        Debug.Log("Actions this update: " + _result.UpdateActions.Count);
        if (_timer > 1)
        {
           
            foreach (var action in _result.UpdateActions)
            {
                //if (action != null)
                //    action.Execute();
            }
            _timer = 0;
        }

        _result = null;
    }

}
