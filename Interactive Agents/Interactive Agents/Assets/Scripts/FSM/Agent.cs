﻿using System.Collections;
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
    private Node moveNext;

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
        pathFinder = GameObject.Find("PathingManager").GetComponent<Pathfinding>();
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
                    movementPath = pathFinder.getPath(transform.position, nextTarget);
                    state = State.Moving;

                    //tell that mission it is taken
                    mission.ActiveAgent = this;
                    mission.setState("Active");

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
                    movementPath = pathFinder.getPath(transform.position, nextTarget);
                    state = State.Moving;


                    //tell that mission it is taken
                    training.ActiveAgent = this;
                    training.setState("Active");

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
        // if there is no current path, get one
        if (movementPath == null)
        {
            movementPath = pathFinder.getPath(transform.position, nextTarget);
        }

        if (movementPath.Count == 0)
        {
            movementPath = pathFinder.getPath(transform.position, nextTarget);
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
            transform.position = Vector3.Lerp(moveFrom, moveNext.position, moveAlpha);
            moveAlpha -= Time.deltaTime;
        }

        if (nextTarget == moveFrom)
        {
            if (trainingTarget != null) state = State.Training;
            if (activeMission != null) state = State.Mission;
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
