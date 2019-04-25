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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.isWall)
                {
                    Gizmos.color = Color.white;
                }

                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));


            }
        }
    }
}
