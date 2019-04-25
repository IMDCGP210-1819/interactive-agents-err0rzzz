using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costs
{
    public int g { get; set; }
    public int h { get; set; }
    public int f { get; set; }
    
    public void SetCosts (int gCost, int hCost)
    {
        g = gCost;
        h = hCost;
        f = gCost + hCost;
    }
}

public class Node
{
    public int gridX, gridY;
    public bool isWall;
    public Vector3 position;
    public Node parent;
    public Costs costs;

    public Node (bool inWall, Vector3 inPos, int inGridX, int inGridY)
    {
        isWall = inWall;
        position = inPos;
        gridX = inGridX;
        gridY = inGridY;
    }

}


