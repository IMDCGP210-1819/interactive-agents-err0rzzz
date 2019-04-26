using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPos;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool Wall = true;

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, wallMask))
                {
                    Wall = false;
                }
                grid[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 posIn)
    {
        float xPoint = ((posIn.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float yPoint = ((posIn.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];

    }

    public List<Node> GetNeighbors(Node inNode)
    {
        List<Node> neighborNodes = new List<Node>();
        int xCheck;
        int yCheck;


        // right
        xCheck = inNode.gridX + 1;
        yCheck = inNode.gridY;
        if ((xCheck >= 0 && xCheck < gridSizeX) && (yCheck >= 0 && yCheck < gridSizeY))
        {
            neighborNodes.Add(grid[xCheck, yCheck]);
        }

        // left
        xCheck = inNode.gridX - 1;
        yCheck = inNode.gridY;
        if ((xCheck >= 0 && xCheck < gridSizeX) && (yCheck >= 0 && yCheck < gridSizeY))
        {
            neighborNodes.Add(grid[xCheck, yCheck]);
        }

        // up
        xCheck = inNode.gridX;
        yCheck = inNode.gridY + 1;
        if ((xCheck >= 0 && xCheck < gridSizeX) && (yCheck >= 0 && yCheck < gridSizeY))
        {
            neighborNodes.Add(grid[xCheck, yCheck]);
        }

        // down
        xCheck = inNode.gridX;
        yCheck = inNode.gridY - 1;
        if ((xCheck >= 0 && xCheck < gridSizeX) && (yCheck >= 0 && yCheck < gridSizeY))
        {
            neighborNodes.Add(grid[xCheck, yCheck]);
        }

        return neighborNodes;
    }



    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));//Draw a wire cube with the given dimensions from the Unity inspector

        if (grid != null)//If the grid is not empty
        {
            foreach (Node n in grid)//Loop through every node in the grid
            {
                if (n.isWall)//If the current node is a wall node
                {
                    Gizmos.color = Color.white;//Set the color of the node
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the node
                }


                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that node
                    }

                }


                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - distance));//Draw the node at the position of the node.
            }
        }
    }
}