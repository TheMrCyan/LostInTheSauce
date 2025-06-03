using UnityEngine;
using System.Collections.Generic;

public class S_T_MazeGenerator : MonoBehaviour
{
    public static S_T_MazeGenerator Instance { get; private set; }

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public Transform player;
    public int width = 40;
    public int height = 24;
    public int kitchenSize = 6;
    private int kitchenX;
    private int kitchenY;
    public float scale = 1;
    public List<Vector2> validItemSpaces;
    private GameObject[,] wallObjects;
    public Sprite[] wallSprites;

    private bool[,] maze;  // true = path, false = wall

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        kitchenX = width / 2 - kitchenSize / 2;
        kitchenY = height / 2 - kitchenSize / 2;

        wallObjects = new GameObject[width, height];

        validItemSpaces = new List<Vector2>();
        player.position = new Vector3(scale * (width / 2), scale * (height / 2), 0);

        floorPrefab.gameObject.transform.localScale = new Vector3(scale, scale, 1);
        wallPrefab.gameObject.transform.localScale = new Vector3(scale, scale, 1);

        maze = new bool[width, height];
        GenerateMaze();

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
                    validItemSpaces.Add(new Vector2(x,y));
                }
                else
                {
                    GameObject wall = Instantiate(wallPrefab, new Vector3(x * scale, y * scale, 0), Quaternion.identity, transform);
                    wallObjects[x, y] = wall; // Store wall in 2D array
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (wallObjects[x, y] != null)
                {
                    int bitmask = GetWallBitmask(x, y);
                    if (bitmask >= 0 && bitmask < wallSprites.Length)
                    {
                        wallObjects[x, y].GetComponent<SpriteRenderer>().sprite = wallSprites[bitmask];
                    }
                    else
                    {
                        Debug.LogWarning($"Bitmask {bitmask} out of range at ({x},{y})");
                    }
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

    int GetWallBitmask(int x, int y)
    {
        int bitmask = 0;

        if (IsWall(x, y + 1)) bitmask |= 1;   // North (Up)
        if (IsWall(x + 1, y)) bitmask |= 2;   // East (Right)
        if (IsWall(x, y - 1)) bitmask |= 4;   // South (Down)
        if (IsWall(x - 1, y)) bitmask |= 8;   // West (Left)

        return bitmask;
    }

    bool IsWall(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
            return false; // Treat out-of-bounds as non-wall
        if (x >= kitchenX && x < kitchenX + kitchenSize &&
        y >= kitchenY && y < kitchenY + kitchenSize)
            return false;
        return !maze[x, y]; // Maze stores paths as true, walls as false
    }

    private void OnDestroy()
    {
        floorPrefab.gameObject.transform.localScale = Vector3.one;
        wallPrefab.gameObject.transform.localScale = Vector3.one;
    }
}