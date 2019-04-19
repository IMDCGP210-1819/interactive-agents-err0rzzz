﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameObject blackboardMan;
    public Blackboard blackboard;

    // stats for this agent
    public int intel, dex, str;

    // either Training or Mission target
    public Mission ActiveTarget;

    // FSM set up
    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Start()
    {
        blackboard = blackboardMan.GetComponent<Blackboard>();
    }
    private void Update()
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
        foreach (Mission mission in Blackboard.Missions)
        {

            if (!mission.checkActive()) ActiveTarget = mission;
            state = State.Mission;
            
        }
    }
    void MissionBehavior()
    {

    }
    void MovingBehavior()
    {

    }

    void TrainingBehavior()
    {

    }

}
