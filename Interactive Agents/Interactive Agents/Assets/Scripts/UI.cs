using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text selected, nametext, dex, str, intel, context1Label, context2Label, state, context1, context2;
    public Button buyRus, buyChi;

    public GameObject M1, M2, M3, M4;
    private Mission Mission1, Mission2, Mission3, Mission4;

    public Text mission1State, mission1Percent;
    public Text mission2State, mission2Percent;
    public Text mission3State, mission3Percent;
    public Text mission4State, mission4Percent;

    private void Start()
    {
        Mission1 = M1.GetComponent<Mission>();
        Mission2 = M2.GetComponent<Mission>();
        Mission3 = M3.GetComponent<Mission>();
        Mission4 = M4.GetComponent<Mission>();
    }

    //private void Start()
    //{
    //    foreach (Mission M in Blackboard.Missions)
    //    {
    //        switch (M.myName)
    //        {
    //            case "UK":
    //                Mission1 = M;
    //                break;

    //            case "France":
    //                Mission2 = M;
    //                break;
    //            case "Russia":
    //                Mission3 = M;
    //                break;
    //            case "China":
    //                Mission4 = M;
    //                break;
    //        }
    //    }
    //}

    private void Update()
    {
        mission1State.text = Mission1.state.ToString();
        mission2State.text = Mission2.state.ToString();
        mission3State.text = Mission3.state.ToString();
        mission4State.text = Mission4.state.ToString();

        if (Mission1.state == Mission.State.Idle)
        {
            mission1Percent.text = (Mission1.waitTimer / Mission1.waitTime*100).ToString("n2") + "%";
        }
        if (Mission1.state == Mission.State.Active)
        {
            mission1Percent.text = (Mission1.missionTimer / Mission1.missionTime * 100).ToString("n2") + "%";
        }

        if (Mission2.state == Mission.State.Idle)
        {
            mission2Percent.text = (Mission2.waitTimer / Mission2.waitTime * 100).ToString("n2") + "%";
        }
        if (Mission2.state == Mission.State.Active)
        {
            mission2Percent.text = (Mission2.missionTimer / Mission2.missionTime * 100).ToString("n2") + "%";
        }

        if (Mission3.state == Mission.State.Idle)
        {
            mission3Percent.text = (Mission3.waitTimer / Mission3.waitTime * 100).ToString("n2") + "%";
        }
        if (Mission3.state == Mission.State.Active)
        {
            mission3Percent.text = (Mission3.missionTimer / Mission3.missionTime * 100).ToString("n2") + "%";
        }

        if (Mission4.state == Mission.State.Idle)
        {
            mission4Percent.text = (Mission4.waitTimer / Mission4.waitTime * 100).ToString("n2") + "%";
        }
        if (Mission4.state == Mission.State.Active)
        {
            mission4Percent.text = (Mission4.missionTimer / Mission4.missionTime * 100).ToString("n2") + "%";
        }

    }

    public void UpdateUI(string selectedS, string nametextS, string dexS, string strS, 
                         string intelS, string context1LabelS, string context2LabelS, 
                         string stateS, string context1S, string context2S)
    {
        selected.text = selectedS;
        nametext.text = nametextS;
        dex.text = dexS;
        str.text = strS;
        intel.text = intelS;
        context1Label.text = context1LabelS;
        context2Label.text = context2LabelS;
        state.text = stateS;
        context1.text = context1S;
        context2.text = context2S;
    }

    public void buyMission(string missionName)
    {
        foreach (Mission m in Blackboard.Missions)
        {
            if (m.myName == missionName)
            {
                m.isBought = true;
                if (missionName == "Russia")
                {
                    buyRus.interactable = false;
                }
                if (missionName == "China")
                {
                    buyChi.interactable = false;
                }
                break;
            }
                                   
        }
    }
}
