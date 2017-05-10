using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    public abstract class Renderer : ScriptableObject
    {
        public abstract GameObject render(int[,] maze, List<point> path);
    }
}