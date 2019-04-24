using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    // requirements for this mission
    public int intel, dex, str;

    // timers for mission
    public float missionTime, waitTime;
    private float missionTimer = 0, waitTimer = 0;

    public Agent ActiveAgent;

    // sprite setup for states
    public SpriteRenderer spriteRen;

    public Sprite idleSprite, pendingSprite, activeSprite;

    public enum State { Active, Pending, Idle}
    public State state = State.Idle;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

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
        spriteRen.sprite = idleSprite;

        waitTimer += Time.deltaTime;
        print(waitTimer.ToString());
        if (waitTimer >= waitTime)
        {
            state = State.Pending;
            waitTimer = 0;
        }    

    }
    void PendingBehavior()
    {
        spriteRen.sprite = pendingSprite;
    }
    void ActiveBehavior()
    {
        spriteRen.sprite = activeSprite;

        missionTimer += Time.deltaTime;
        print(missionTimer.ToString());
        if (missionTimer >= missionTime)
        {
            print("mission complete!");
            ActiveAgent.activeMission = null;
            ActiveAgent = null;
            state = State.Idle;
            missionTimer = 0;
        }
    }

    public bool checkAvalible()
    {
        if (ActiveAgent == null && state == State.Pending) return true;
        else return false;
    }    

    //public bool checkComplete()
    //{
    //    return false;
    //}

    public void setState(string inV)
    {
        switch (inV)
        {
            case "Idle":
                state = State.Idle;
                break;

            case "Pending":
                state = State.Pending;
                break;

            case "Active":
                state = State.Active;
                break;
        }
    }
}
