using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameObject blackboardMan;
    public Blackboard blackboard;

    // stats for this agent
    public int intel, dex, str;
    private bool lastMissionSuccess = true;

    // either Training or Mission target
    public Mission ActiveTarget;
    public Training TrainingTarget;

    // FSM set up
    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Start()
    {
        blackboard = blackboardMan.GetComponent<Blackboard>();
    }
    public void Think()
    {
        print("thinking" + state.ToString());
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
                //if the mission is not active
                if (!mission.checkActive())
                {
                    //make this agent take this mission
                    ActiveTarget = mission;
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
                    TrainingTarget = trainig;
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

    }
    void MovingBehavior()
    {

    }

    void TrainingBehavior()
    {

    }

}
