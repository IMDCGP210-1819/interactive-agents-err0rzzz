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

    //private void Update()
    //{
    //    FindPath(startPos.position, targetPos.position);
    //}

    public List<Node> getPath(Vector3 startPos, Vector3 endPos)
    {
        FindPath(startPos, endPos);
        List<Node> retList = new List<Node>();
        retList = grid.FinalPath;
            
        return retList;
    }

    public void FindPath(Vector3 startSearch, Vector3 targetSearch)
    {
        Node startNode = grid.NodeFromWorldPosition(startSearch);
        Node targetNode = grid.NodeFromWorldPosition(targetSearch);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(startNode);

        while (OpenList.Count>0)
        {
            Node currentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].fCost < currentNode.fCost || (OpenList[i].fCost == currentNode.fCost && OpenList[i].hCost < currentNode.hCost))
                {
                    currentNode = OpenList[i];
                }
            }
            OpenList.Remove(currentNode);
            ClosedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
                break;
            }

            foreach (Node neighborNode in grid.GetNeighbors(currentNode))
            {
                if (!neighborNode.isWall || ClosedList.Contains(neighborNode))
                {
                    continue;
                }

                int MoveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);
                
                if (MoveCost < neighborNode.fCost || !OpenList.Contains(neighborNode))
                {
                    neighborNode.gCost = MoveCost;
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.parent = currentNode;

                    if (!OpenList.Contains(neighborNode))
                    {
                        OpenList.Add(neighborNode);
                    }
                }
            }


        }
    }

    void GetFinalPath(Node start, Node end)
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
        int x = Mathf.Abs(inA.gridX - inB.gridX);
        int y = Mathf.Abs(inA.gridY - inB.gridY);

        return x + y;
    }
}

