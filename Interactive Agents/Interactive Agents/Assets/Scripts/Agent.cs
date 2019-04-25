using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private GameObject blackboardMan;
    private Blackboard blackboard;
    private UI ui;

    // stats for this agent
    public string myName;
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
        ui = GameObject.Find("GUI").GetComponent<UI>();
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
                    state = State.Training;


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
