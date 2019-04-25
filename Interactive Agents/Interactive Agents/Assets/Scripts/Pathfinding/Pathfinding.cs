using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPos;
    public Transform targetPos;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }

    void FindPath(Vector3 start, Vector3 target)
    {
        Node startNode = grid.NodeFromWorldPosition(start);
        Node targetNode = grid.NodeFromWorldPosition(target);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(startNode);

        while (OpenList.Count>0)
        {
            Node currentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].costs.f < currentNode.costs.f || (OpenList[i].costs.f == currentNode.costs.f && OpenList[i].costs.h < currentNode.costs.h))
                {
                    currentNode = OpenList[i];
                }
            }

            OpenList.Remove(currentNode);
            ClosedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach (Node optionNode in grid.GetNeighbors(currentNode))
            {
                if (!optionNode.isWall || ClosedList.Contains(optionNode))
                {
                    continue;
                }
                int MoveCost = currentNode.costs.g + GetManhattenDistance(currentNode, optionNode);

                if (MoveCost < optionNode.costs.g || !OpenList.Contains(optionNode))
                {
                    optionNode.costs.g = MoveCost;
                    optionNode.costs.h = GetManhattenDistance(optionNode, targetNode);
                    optionNode.parent = currentNode;

                    if (!OpenList.Contains(optionNode))
                    {
                        OpenList.Add(optionNode);
                    }
                }
            }


        }
    }

    void GetFinalPath( Node start, Node end)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = end;

        while (CurrentNode != start)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }

        FinalPath.Reverse();
        grid.FinalPath = FinalPath;
    }

    private int GetManhattenDistance(Node inA, Node inB)
    {
        int ix = Mathf.Abs(inA.gridX - inB.gridX);
        int iy = Mathf.Abs(inA.gridY - inB.gridY);

        return ix + iy;
    }
}

