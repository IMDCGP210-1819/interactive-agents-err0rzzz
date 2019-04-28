using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    public float fleeDistance, wallDistance;
    public float fleeWeight, seekWeight, wallAvoidWeight, speed;

    public LayerMask wallMask;

    private float wallweightInclude;

    private GameObject Mainframe;

    private void Start()
    {
        Mainframe = GameObject.Find ("Mainframe");
    }

    public Vector3 CalculateMove()
        {
        
        // final vector for movement
        Vector3 calcMove = new Vector3(0,0,0);

        // flee agents within range
        foreach (Agent agent in Blackboard.Agents)
        {

            Vector3 heading = (agent.transform.position - transform.position);
  

            if (heading.sqrMagnitude < fleeDistance * fleeDistance)
            {
                //only calculate magnitude if needed as cup hugry according to unity docs
                float distance = heading.magnitude;
                calcMove -= heading / distance;
            }

        }
        // weight that 
        calcMove *= fleeWeight;

        // check if theres a wall in range and avoid it
        Collider2D contact = Physics2D.OverlapCircle(transform.position, wallDistance, wallMask);
        if (contact != null)
        {
            Vector3 heading = (contact.transform.position - transform.position);
            float distance = heading.magnitude;
            calcMove += (heading / distance) * wallAvoidWeight;
            wallweightInclude = wallAvoidWeight;
        }
        else wallweightInclude = 0;


        // seek mainframe
        calcMove += (Mainframe.transform.position - transform.position) * seekWeight;
        calcMove /= (fleeWeight + seekWeight + wallweightInclude);
        
        return calcMove;
    }

    void Update()
    {
        transform.Translate(CalculateMove()*Time.deltaTime*speed);
    }
}
