using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{

    // benefitd of this training
    public int intel, dex, str;
    private int trainingLevel = 0;

    // timers for mission
    public float trainingTime, waitTime;
    private float trainingTimer = 0, waitTimer = 0;

    public string myName;

    public Agent ActiveAgent;

  //  private GameObject blackboardMan;
  //  private Blackboard blackboard;
    private UI ui;

    // sprite setup for states
    public SpriteRenderer spriteRen;

    public Sprite idleSprite, pendingSprite, activeSprite;

    public enum State { Active, Pending, Idle }
    public State state = State.Idle;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
     //   blackboard = GameObject.Find("BlackboardManager").GetComponent<Blackboard>();
        ui = GameObject.Find("GUI").GetComponent<UI>();
    }

    // checks if this training can be allocated to an Agent
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
        spriteRen.sprite = idleSprite;

        waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            state = State.Pending;
            waitTimer = 0;
        }

    }
    private void PendingBehavior()
    {
        spriteRen.sprite = pendingSprite;
    }
    private void ActiveBehavior()
    {
        spriteRen.sprite = activeSprite;

        trainingTimer += Time.deltaTime;
        if (trainingTimer >= trainingTime)
        {
            CompleteTraining();
            trainingTimer = 0;
        }
    }

    // checks if the Agent has the stats to win the mission
    // then updates Agent and Mission
    private void CompleteTraining()
    {
        print("Training Complete");

        ActiveAgent.str += str + trainingLevel;
        ActiveAgent.intel += intel + trainingLevel;
        ActiveAgent.dex += dex  + trainingLevel;

        ActiveAgent.lastMissionSuccess = true;
        ActiveAgent.trainingTarget = null;

        ActiveAgent = null;

        state = State.Idle;
    }

    private void OnMouseDown()
    {
        ui.UpdateUI(this.GetType().ToString(), myName, dex.ToString(), str.ToString(), intel.ToString(), "Training Level: ", "Completion: ", state.ToString(), trainingLevel.ToString(), (trainingTimer/trainingTime*100).ToString("n2")+"%");
    }
}

