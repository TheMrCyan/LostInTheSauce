using UnityEngine;
using System.Collections.Generic;

public class S_T_MazeGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public Transform player;
    public int width = 40;
    public int height = 24;
    public int kitchenSize = 6;
    public float scale = 1;

    private bool[,] maze;  // true = path, false = wall

    void Start()
    {
        player.position = new Vector3(scale * (width / 2), scale * (height / 2), 0);

        floorPrefab.gameObject.transform.localScale = new Vector3(scale, scale, 1);
        wallPrefab.gameObject.transform.localScale = new Vector3(scale, scale, 1);

        maze = new bool[width, height];
        GenerateMaze();

        // Calculate kitchen position
        int kitchenX = width / 2 - kitchenSize / 2;
        int kitchenY = height / 2 - kitchenSize / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Kitchen area: always floor
                if (x >= kitchenX && x < kitchenX + kitchenSize &&
                    y >= kitchenY && y < kitchenY + kitchenSize)
                {
                    Instantiate(floorPrefab, new Vector3(x * scale, y * scale, 0), Quaternion.identity, transform);
                }
                else if (maze[x, y])
                {
                    Instantiate(floorPrefab, new Vector3(x * scale, y * scale, 0), Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(wallPrefab, new Vector3(x * scale, y * scale, 0), Quaternion.identity, transform);
                }
            }
        }
    }

    void GenerateMaze()
    {
        // Initialize all cells as walls
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                maze[x, y] = false;

        // Start recursive backtracking from random position
        Vector2Int start = new Vector2Int(1, 1);
        RecursiveBacktrack(start);
    }

    void RecursiveBacktrack(Vector2Int pos)
    {
        maze[pos.x, pos.y] = true;

        foreach (Vector2Int dir in ShuffleDirections())
        {
            Vector2Int next = pos + dir * 2;
            if (IsInBounds(next) && !maze[next.x, next.y])
            {
                Vector2Int wall = pos + dir;
                maze[wall.x, wall.y] = true;
                RecursiveBacktrack(next);
            }
        }
    }

    List<Vector2Int> ShuffleDirections()
    {
        List<Vector2Int> dirs = new List<Vector2Int> {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };

        // Shuffle the list
        for (int i = 0; i < dirs.Count; i++)
        {
            Vector2Int temp = dirs[i];
            int rand = Random.Range(i, dirs.Count);
            dirs[i] = dirs[rand];
            dirs[rand] = temp;
        }

        return dirs;
    }

    bool IsInBounds(Vector2Int pos)
    {
        return pos.x > 0 && pos.x < width - 1 && pos.y > 0 && pos.y < height - 1;
    }

    private void OnDestroy()
    {
        floorPrefab.gameObject.transform.localScale = Vector3.one;
        wallPrefab.gameObject.transform.localScale = Vector3.one;
    }
}

