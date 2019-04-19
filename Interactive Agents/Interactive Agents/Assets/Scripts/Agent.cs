using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public int intel, dex, str;

    public GameObject ActiveMission;

    public enum State {Idle, Mission, Training, Moving}
    State state = State.Idle;

    private void Update()
    {
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

    void IdleBehavior()
    {

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
