using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public static Mission[] Missions;
    public static Training[] Trainings;
    public static Agent[] Agents;

    public int money;

    // Start is called before the first frame update
    void Start()
    {
        Missions = FindObjectsOfType<Mission>();
        Trainings = FindObjectsOfType<Training>();
        Agents = FindObjectsOfType<Agent>();

    }

    private void Update()
    {
        foreach (Agent agent in Agents)
        {
            agent.Think();
        }

        foreach (Mission mission in Missions)
        {
            mission.Think();
        }
    }

    public void setMoney(int a)
    {
        money += a;
    }

    // disused function to check and return missions
    // was building as a fix to a bug elsewhere
    
    //public Mission getMission()
    //{
    //    foreach (Mission m in Missions)
    //    {
    //        if (!m.checkActive())
    //        {
    //                   break;
    //        }
    //    }
    //    else return null;
    //}
}
