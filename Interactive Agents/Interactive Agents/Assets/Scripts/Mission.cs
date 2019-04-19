﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    public int intel, dex, str;
    public float missionTime, waitTime;

    public GameObject ActiveAgent;

    public enum State { Active, Pending, Idle}
    State state = State.Idle;

    private void Update()
    {
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
