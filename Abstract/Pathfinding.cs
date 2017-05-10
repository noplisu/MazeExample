using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    public abstract class Pathfinding : ScriptableObject
    {
        public abstract int[,] calculate(int[,] map, out List<point> path);
    }

    public struct point
    {
        public int x;
        public int y;

        public point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
