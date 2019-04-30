using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    

    // requirements for this mission
    public int intel, dex, str;
    private int missionsComplete = 0;

    public string myName;
    public bool isBought = true;

    // timers for mission
    public float missionTime, waitTime;
    public float missionTimer = 0, waitTimer = 0;

    public Agent ActiveAgent;

   // private GameObject blackboardMan;
    private Blackboard blackboard;
    private UI ui;

    // sprite setup for states
   // public SpriteRenderer spriteRen;

  //  public Sprite idleSprite, pendingSprite, activeSprite;

    public enum State { Active, Pending, Idle}
    public State state = State.Pending;

    private void Start()
    {
      //  spriteRen = GetComponent<SpriteRenderer>();
        blackboard = GameObject.Find("BlackboardManager").GetComponent<Blackboard>();
        ui = GameObject.Find("GUI").GetComponent<UI>();
    }

    // checks if this mission can be allocated to an Agent
    public bool checkAvalible()
    {
        if (ActiveAgent == null && state == State.Pending) return true;
        else return false;
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

    // main FSM operation
    public void Think()
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
    private void IdleBehavior()
    {
        //spriteRen.sprite = idleSprite;

        if (isBought) waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            state = State.Pending;
            waitTimer = 0;
        }    

    }
    private void PendingBehavior()
    {
      //  spriteRen.sprite = pendingSprite;
    }
    private void ActiveBehavior()
    {
     //   spriteRen.sprite = activeSprite;

        missionTimer += Time.deltaTime;

        if (missionTimer >= missionTime)
        {
            CompleteMission();
            missionTimer = 0;
        }
    }

    // checks if the Agent has the stats to win the mission
    // then updates Agent and Mission
    private void CompleteMission()
    {
        if (ActiveAgent.str >= str && ActiveAgent.intel >= intel && ActiveAgent.dex >= dex)
        {
            print("mission complete: SUCCESS!");
            ActiveAgent.lastMissionSuccess = true;
            blackboard.setMoney(50);
            missionsComplete++;
            IncreaseDifficulty();
        }

        else
        {
            print("mission complete: FAILURE!");
            ActiveAgent.lastMissionSuccess = false;
        }

        ActiveAgent.activeMission = null;
        ActiveAgent = null;
        state = State.Idle;
    }

    // randomly increases one of the stats by the number of successes
    private  void IncreaseDifficulty()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                intel += missionsComplete;
                break;

            case 1:
                str += missionsComplete;
                break;

            case 2:
                dex += missionsComplete;
                break;
        }

    }

    private void OnMouseDown()
    {
        ui.UpdateUI(this.GetType().ToString(), myName, dex.ToString(), str.ToString(), intel.ToString(), "Missions Complere: ", "Time left: ", state.ToString(), missionsComplete.ToString(), missionTimer.ToString());
    }




}
