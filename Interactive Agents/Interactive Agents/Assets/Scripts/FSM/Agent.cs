using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private GameObject blackboardMan;
    private Blackboard blackboard;
    private UI ui;
    private Pathfinding pathFinder;
    private Vector3 mainframe;

    // stats for this agent
    public string myName;
    public int intel, dex, str;
    public bool lastMissionSuccess = true;

    private List<Node> movementPath = new List<Node>();
    float moveAlpha;
    private Vector3 nextTarget, moveFrom;
    private Node moveNext, moveLast;
    private bool newTarget;

    private float waitTime, waitTimer;

    // either Training or Mission target
    public Mission activeMission;
    public Training trainingTarget;
    public Chair myChair;

    public enum Movetarget { Mainframe, Mission, Training, Table}
    Movetarget MoveTarget = Movetarget.Mainframe;

    // FSM set up
    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Start()
    {
        blackboard = GameObject.Find("BlackboardManager").GetComponent<Blackboard>();
        ui = GameObject.Find("GUI").GetComponent<UI>();
        pathFinder = GetComponent<Pathfinding>();
        mainframe = GameObject.Find("Mainframe").GetComponent<Transform>().position;

        waitTime = (float)Random.Range(1, 5);
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

        switch (MoveTarget)
        {
            case Movetarget.Mission:
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
                            MoveTarget = Movetarget.Mission;

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
                            MoveTarget = Movetarget.Training;

                            //tell that mission it is taken
                            training.ActiveAgent = this;
                            break;
                        }
                    }
                }


                return;

            case Movetarget.Mainframe:
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    state = State.Moving;

                    newTarget = true;
                    nextTarget = mainframe;
                    MoveTarget = Movetarget.Mainframe;

                    waitTimer = 0;
                    waitTime = (float)Random.Range(1, 5);
                }
                return;

            case Movetarget.Table:
                return;

            case Movetarget.Training:
                return;
        }


        
    }
    void MissionBehavior()
    {
        if (activeMission == null)
        {
            
            state = State.Moving;
            MoveTarget = Movetarget.Table;

            foreach (Chair chair in Blackboard.Chairs)
            {
                //if chair is avalible
                if (chair.isAvalible)
                {

                    nextTarget = chair.transform.position;
                    newTarget = true;
                    state = State.Moving;
                    MoveTarget = Movetarget.Table;
                    chair.isAvalible = false;
                    myChair = chair;
                    break;
                }
            }
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
            // If we have arrivd at the destination
            if (moveNext == moveLast)
            {
                switch (MoveTarget)
                {
                    case Movetarget.Mainframe:
                        state = State.Idle;
                        MoveTarget = Movetarget.Mission;
                        if (myChair!= null) { 
                        myChair.isAvalible = true;
                        myChair = null;}
                        return;

                    case Movetarget.Training:
                        if (trainingTarget != null)
                        {
                            state = State.Training;
                            trainingTarget.setState("Active");
                            movementPath.Clear();
                            return;
                        }
                        return;

                    case Movetarget.Mission:
                        if (activeMission != null)
                        {
                            state = State.Mission;
                            activeMission.setState("Active");
                            movementPath.Clear();
                            return;
                        }
                        return;

                    case Movetarget.Table:
                        state = State.Idle;
                        MoveTarget = Movetarget.Mainframe;
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
            {

                state = State.Moving;
                MoveTarget = Movetarget.Table;

                foreach (Chair chair in Blackboard.Chairs)
                {
                    //if chair is avalible
                    if (chair.isAvalible)
                    {

                        nextTarget = chair.transform.position;
                        newTarget = true;
                        state = State.Moving;
                        MoveTarget = Movetarget.Table;
                        chair.isAvalible = false;
                        myChair = chair;
                        break;
                    }
                }
            }

        }

    }

    private void OnMouseDown()
    {
        ui.UpdateUI(this.GetType().ToString(), myName, dex.ToString(), str.ToString(), intel.ToString(), "Last mission success? ", "unused", state.ToString(), lastMissionSuccess.ToString(), "unused");
    }
}
