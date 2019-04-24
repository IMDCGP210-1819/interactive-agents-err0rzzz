using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private GameObject blackboardMan;
    private Blackboard blackboard;

    // stats for this agent
    public string name;
    public int intel, dex, str;
    public bool lastMissionSuccess = true;

    // either Training or Mission target
    public Mission activeMission;
    public Training trainingTarget;

    // FSM set up
    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Start()
    {
        blackboard = GameObject.Find("BlackboardManager").GetComponent<Blackboard>();
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
                    state = State.Mission;

                    //tell that mission it is taken
                    mission.ActiveAgent = this;
                    mission.setState("Active");

                    break;
                }
            }

            // all missions tested and none avalible, check training
            foreach (Training trainig in Blackboard.Trainings)
            {
                //if the mission is not active
                if (!trainig.checkActive())
                {
                    //make this agent take this mission
                    trainingTarget = trainig;
                    state = State.Training;


                    //tell that mission it is taken
                    trainig.ActiveAgent = this;
                    
                    break;
                }
            }
        }

        if (!lastMissionSuccess)
        {

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

    }

    void TrainingBehavior()
    {

    }

}
