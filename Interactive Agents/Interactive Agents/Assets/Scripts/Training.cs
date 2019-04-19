using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    //training modify Agent's stats
    public int intel, dex, str;

    //time for training / between training
    public float trainingTime, waitTime;

    //agent using training
    public GameObject ActiveAgent;

    // FSM setup
    public enum State { Active, Pending, Idle }
    State state = State.Idle;

    private void Update()
    {
        // FSM main operation
        switch (state)
        {
            case State.Idle:
                IdleBehavior();
                break;

            case State.Pending:
                PendingBehavior();
                break;

            case State.Active:
                ActiveBehavior();
                break;
        }
    }


    // Behaviors for each state
    void IdleBehavior()
    {

    }
    void PendingBehavior()
    {

    }
    void ActiveBehavior()
    {

    }
}
