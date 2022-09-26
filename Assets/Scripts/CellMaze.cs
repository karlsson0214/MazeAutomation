using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CellMaze : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    private int[,] maze;
    //width and hight of maze: 9 + 2 n, where n = 0, 1, 2, ...
    private const int XSIZE = 17
        ;
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

        // Setup
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
        for (int y = 2; y < YSIZE - 2; y = y + 2)
        {
            for (int x = 2; x < XSIZE - 2; x = x + 2)
            {
                maze[x, y] = WALL;
            }
        }
        for (int y = 1; y < YSIZE; y = y + 2)
        {
            for (int x = 1; x < XSIZE; x = x + 2)
            {
                maze[x, y] = FLOOR;
            }
        }
        // build the rest
        CleverRandomWalls();
        
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
    private void RandomWalls()
    {
        for (int y = 1; y < YSIZE - 1; y = y + 1)
        {
            for (int x = 1; x < XSIZE - 1; x = x + 1)
            {
                if (maze[x, y] == EMPTY)
                {
                    float probability = Random.Range(0, 1f);
                    if (probability < 0.3)
                    {
                        maze[x, y] = WALL;
                    }
                    else
                    {
                        maze[x, y] = FLOOR;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Traverse all odd rows where the column also is odd. One cell at a time. 
    /// if cell is floor
    ///     // check neighbours East, N, W, S
    ///     // and set UNset cells
    ///     if 2 walls => add 2 floors
    ///     if 1 walls => randomize rest(wall or floor) but at least 2 floors
    ///     if 0 walls => randomize rest(wall or floor) but at least 2 floors(Will never happend with setup above)
    /// </summary>
    private void CleverRandomWalls()
    {
        for (int y = 1; y < YSIZE; y = y + 2)
        {
            for (int x = 1; x < XSIZE; x = x + 2)
            {
                // cell is floor
                List<Coordinate> neighbours = new List<Coordinate>();
                neighbours.Add(new Coordinate(x - 1, y));
                neighbours.Add(new Coordinate(x + 1, y));
                neighbours.Add(new Coordinate(x, y - 1));
                neighbours.Add(new Coordinate(x, y + 1));

                int noWalls = 0;
                foreach(Coordinate cell in neighbours)
                {
                    if (maze[cell.x, cell.y] == WALL)
                    {
                        ++noWalls;
                    }
                    
                }
                Debug.Log("coordinate: " + x + ", " + y + "\nno Walls: " + noWalls);
                if (noWalls == 2)
                {
                    foreach(Coordinate cell in neighbours)
                    {
                        if (maze[cell.x, cell.y] == EMPTY)
                        {
                            maze[cell.x, cell.y] = FLOOR;
                        }
                    }
                }
                else if (noWalls < 2)
                {
                    // remove walls from list
                    for (int i = 0; i < neighbours.Count; ++i)
                    {
                        if (maze[neighbours[i].x, neighbours[i].y] == WALL)
                        {
                            neighbours.RemoveAt(i);
                        }
                    }
                    while(neighbours.Count > 2)
                    {
                        // select neighbour
                        int randomNeighbour = Random.Range(0, neighbours.Count);
                        float probability = Random.Range(0, 1f);
                        // probability of wall
                        if (probability < 0.6f)
                        {
                            if (maze[neighbours[randomNeighbour].x, neighbours[randomNeighbour].y] == EMPTY)
                            {
                                // set value and remove from list
                                maze[neighbours[randomNeighbour].x, neighbours[randomNeighbour].y] = WALL;
                                neighbours.RemoveAt(randomNeighbour);
                            }

                        }
                        else
                        {
                            if (maze[neighbours[randomNeighbour].x, neighbours[randomNeighbour].y] == EMPTY)
                            {
                                // set value and remove from list
                                maze[neighbours[randomNeighbour].x, neighbours[randomNeighbour].y] = FLOOR;
                                neighbours.RemoveAt(randomNeighbour);
                            }
                            
                        }
                    }
                    // two cells left
                    foreach (Coordinate cell in neighbours)
                    {
                        maze[cell.x, cell.y] = FLOOR;
                    }
                }
                //maze[x, y] = FLOOR;
            }
        }
    }
}

public class Coordinate
{
    public int x;
    public int y;
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
