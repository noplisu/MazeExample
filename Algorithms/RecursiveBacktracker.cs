using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    [CreateAssetMenu(fileName = "RecursiveBacktracker", menuName = "Algorithms/RecursiveBacktracker", order = 1)]
    public class RecursiveBacktracker : Algorithm
    {
        public override int[,] generate(int w, int h, int seed)
        {
            int[,] maze = new int[w, h];

            for(int i=0; i<maze.GetLength(0); i++)
            {
                for(int j=0; j<maze.GetLength(1); j++)
                {
                    maze[i, j] = 1;
                }
            }

            Random.InitState(seed);

            maze[0, 1] = 2;
            maze[1, 1] = 0;
            dig(maze, 1, 1);
            maze[w - 1, h - 2] = 3;

            return maze;
        }

        void dig(int[,] maze, int x, int y)
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
                switch(direction)
                {
                    case 1:
                        if(x + 2 < maze.GetLength(0) && maze[x + 2, y] == 1)
                        {
                            maze[x + 1, y] = 0;
                            maze[x + 2, y] = 0;
                            dig(maze, x + 2, y);
                        }
                        break;
                    case 2:
                        if (y + 2 < maze.GetLength(1) && maze[x, y + 2] == 1)
                        {
                            maze[x, y + 1] = 0;
                            maze[x, y + 2] = 0;
                            dig(maze, x, y + 2);
                        }
                        break;
                    case 3:
                        if (x - 2 > 0 && maze[x - 2, y] == 1)
                        {
                            maze[x - 1, y] = 0;
                            maze[x - 2, y] = 0;
                            dig(maze, x - 2, y);
                        }
                        break;
                    case 4:
                        if (y - 2 > 0 && maze[x, y - 2] == 1)
                        {
                            maze[x, y - 1] = 0;
                            maze[x, y - 2] = 0;
                            dig(maze, x, y - 2);
                        }
                        break;
                }
            }
        }
    }
}