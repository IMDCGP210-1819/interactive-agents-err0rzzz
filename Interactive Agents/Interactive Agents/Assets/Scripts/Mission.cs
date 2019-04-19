using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    // requirements for this mission
    public int intel, dex, str;

    // timers for mission
    public float missionTime, waitTime;

    public GameObject ActiveAgent;

    public enum State { Active, Pending, Idle}
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
