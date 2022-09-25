using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class CellMaze : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    private int[,] maze;
    //width and hight of maze: 9 + 3 n, where n = 0, 1, 2, ...
    private const int XSIZE = 18;
    private const int YSIZE = 15;

    private const int EMPTY = 0;
    private const int WALL = 1;
    private const int FLOOR = 2;


    // Start is called before the first frame update
    void Start()
    {
        CreateMaze();
        ShowMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateMaze()
    {
        maze = new int[XSIZE, YSIZE];
        for (int x = 0; x < XSIZE; x++)
        {
            // north
            maze[x, 0] = WALL;
            // south
            maze[x, YSIZE - 1] = WALL;
        }
        for (int y = 0; y < YSIZE; y++)
        {
            // west
            maze[0, y] = WALL;
            // east
            maze[XSIZE - 1, y] = WALL;
        }
        for (int y = 1; y < YSIZE; y = y + 2)
        {
            for (int x = 1; x < XSIZE; x = x + 2)
            {
                maze[x, y] = FLOOR;
            }
        }
    }
    private void ShowMaze()
    {
        // show maze in scene: add walls and floors
        for (int y = 0; y < YSIZE; ++y)
        {
            for (int x = 0; x < XSIZE; ++x)
            {
                if (maze[x,y] == WALL)
                {
                    Instantiate(wallPrefab, new Vector2(x, y), Quaternion.identity);
                }
                else if (maze[x,y] == FLOOR)
                {
                    Instantiate(floorPrefab, new Vector2(x, y), Quaternion.identity);
                }
            }
        }
    }
}
