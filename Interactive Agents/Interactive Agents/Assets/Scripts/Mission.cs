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
    }
    void PendingBehavior()
    {
        spriteRen.sprite = pendingSprite;
    }
    void ActiveBehavior()
    {
        spriteRen.sprite = activeSprite;
    }

    public bool checkActive()
    {
        if (ActiveAgent != null && state == State.Pending) return true;
        else return false;
    }    

    public bool checkComplete()
    {
        return false;
    }

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
