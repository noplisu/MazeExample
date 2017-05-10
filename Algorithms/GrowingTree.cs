using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    [CreateAssetMenu(fileName = "GrowingTree", menuName = "Algorithms/GrowingTree", order = 1)]
    public class GrowingTree : Algorithm
    {
        public override int[,] generate(int w, int h, int seed)
        {
            int[,] maze = new int[w, h];

            for (int i=0; i<maze.GetLength(0); i++)
            {
                for(int j=0; j<maze.GetLength(1); j++)
                {
                    maze[i, j] = 1;
                }
            }

            Random.InitState(seed);

            List<Vector2> openList = new List<Vector2>();

            maze[0, 1] = 0;
            maze[1, 1] = 0;
            openList.Add(new Vector2(1, 1));

            do
            {
                for (int i = openList.Count - 1; i >= 0 ; i--)
                {
                    int x = (int)openList[i].x;
                    int y = (int)openList[i].y;
                    Vector2 newField;
                    if(dig(maze, x, y, out newField))
                    {
                        openList.Add(newField);
                        break;
                    }
                    else
                    {
                        openList.Remove(openList[i]);
                    }
                }
            } while (openList.Count > 0);

            for (int i = maze.GetLength(0) - 2; i > 1; i--)
            {
                if(maze[i, maze.GetLength(1) - 2] == 0)
                {
                    maze[0, 1] = 2;
                    maze[i, maze.GetLength(1) - 1] = 3;
                    return maze;
                }
            }

            return maze;
        }

        bool dig(int[,] map, int x, int y, out Vector2 newNode)
        {
            int[] values = { 1, 2, 3, 4 };
            List<int> directions = new List<int>(values);

            for (int i = 0; i < directions.Count; i++)
            {
                int temp = directions[i];
                int randomIndex = Random.Range(i, directions.Count);
                directions[i] = directions[randomIndex];
                directions[randomIndex] = temp;
            }

            foreach (int direction in directions)
            {
                switch (direction)
                {
                    case 1:
                        if (x < map.GetLength(0) - 2 && map[x + 1, y] == 1 && countNeighbours(map, x + 1, y) < 2)
                        {
                            map[x + 1, y] = 0;
                            newNode = new Vector2(x + 1, y);
                            return true;
                        }
                        break;
                    case 2:
                        if (y < map.GetLength(1) - 2 && map[x, y + 1] == 1 && countNeighbours(map, x, y + 1) < 2)
                        {
                            map[x, y + 1] = 0;
                            newNode = new Vector2(x, y + 1);
                            return true;
                        }
                        break;
                    case 3:
                        if (x > 1 && map[x - 1, y] == 1 && countNeighbours(map, x - 1, y) < 2)
                        {
                            map[x - 1, y] = 0;
                            newNode = new Vector2(x - 1, y);
                            return true;
                        }
                        break;
                    case 4:
                        if (y > 1 && map[x, y - 1] == 1 && countNeighbours(map, x, y - 1) < 2)
                        {
                            map[x, y - 1] = 0;
                            newNode = new Vector2(x, y - 1);
                            return true;
                        }
                        break;
                }
            }
            newNode = new Vector2(-1, -1);
            return false;
        }

        int countNeighbours(int[,] map, int x, int y)
        {
            int neighbours = 0;
            if (map[x + 1, y] == 0)
            {
                neighbours++;
            }
            if (map[x, y + 1] == 0)
            {
                neighbours++;
            }
            if (map[x - 1, y] == 0)
            {
                neighbours++;
            }
            if (map[x, y - 1] == 0)
            {
                neighbours++;
            }
            return neighbours;
        }
    }
}