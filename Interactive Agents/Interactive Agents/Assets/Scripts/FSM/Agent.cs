using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private GameObject blackboardMan;
    private Blackboard blackboard;
    private UI ui;
    private Pathfinding pathFinder;

    // stats for this agent
    public string myName;
    public int intel, dex, str;
    public bool lastMissionSuccess = true;

    private List<Node> movementPath = new List<Node>();
    float moveAlpha;
    private Vector3 nextTarget, moveFrom;
    private Node moveNext, moveLast;
    private bool newTarget;

    // either Training or Mission target
    public Mission activeMission;
    public Training trainingTarget;

    // FSM set up
    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Start()
    {
        blackboard = GameObject.Find("BlackboardManager").GetComponent<Blackboard>();
        ui = GameObject.Find("GUI").GetComponent<UI>();
        pathFinder = GetComponent<Pathfinding>();
    }
    public void Think()
    {

        // FSM main operation
        switch (state)
            {
            case State.Idle:
                IdleBehavior();
                break;

            case State.Mission:
                MissionBehavior();
                break;

            case State.Moving:
                MovingBehavior();
                break;

            case State.Training:
                TrainingBehavior();
                break;

        }
    }

    // Behaviors for each state
    void IdleBehavior()
    {
        if (lastMissionSuccess)
        {
            foreach (Mission mission in Blackboard.Missions)
            {
                //if the mission is avalible
                if (mission.checkAvalible())
                {
                    //make this agent take this mission
                    activeMission = mission;
                    trainingTarget = null;
                    nextTarget = mission.transform.position;
                    newTarget = true;
                    state = State.Moving;

                    //tell that mission it is taken
                    mission.ActiveAgent = this;
                    break;
                }
            }                   
            
        }

        else 
        {
            foreach (Training training in Blackboard.Trainings)
            {
                //if training is avalible
                if (training.checkAvalible())
                {
                    //make this agent take this mission
                    trainingTarget = training;
                    activeMission = null;
                    nextTarget = training.transform.position;
                    newTarget = true;
                    state = State.Moving;
                    
                    //tell that mission it is taken
                    training.ActiveAgent = this;                    
                    break;
                }
            }
        }
        
    }
    void MissionBehavior()
    {
        if (activeMission == null)
        {
            
            state = State.Idle;
        }
    }
    void MovingBehavior()
    {

        if (newTarget)
        {
            newTarget = false;
            movementPath = pathFinder.getPath(transform.position, nextTarget);
            if (movementPath.Count!= 0) moveLast = movementPath[movementPath.Count -1];
        }

        if (moveNext != null)
        {
            if (moveNext == moveLast)
            {
                
                if (trainingTarget != null)
                {
                    state = State.Training;
                    trainingTarget.setState("Active");
                    movementPath.Clear();
                    return;
                }

                if (activeMission != null)
                {
                    state = State.Mission;
                    activeMission.setState("Active");
                    movementPath.Clear();
                    return;
                }
            }
        }

            // if ready to set next step
        if (moveAlpha <= 0)
           {
            moveNext = movementPath[0];
            movementPath.RemoveAt(0);
            moveAlpha = 1f;
            moveFrom = transform.position;

           }

        if (movementPath.Count > 0)
        {
            transform.position = Vector3.Lerp(moveFrom, moveNext.position, 1f - moveAlpha);
            moveAlpha -= Time.deltaTime * 2 ;
        }

    }
    void TrainingBehavior()
    {
        if (trainingTarget == null)
        {
            state = State.Idle;
        }

    }

    private void OnMouseDown()
    {
        ui.UpdateUI(this.GetType().ToString(), myName, dex.ToString(), str.ToString(), intel.ToString(), "Last mission success? ", "unused", state.ToString(), lastMissionSuccess.ToString(), "unused");
    }
}
