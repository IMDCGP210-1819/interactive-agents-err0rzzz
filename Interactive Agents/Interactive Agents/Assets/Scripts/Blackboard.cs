using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public static Mission[] Missions;
    public static Training[] Trainings;
    public static Agent[] Agents;

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
    }
}
