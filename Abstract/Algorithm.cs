using UnityEngine;
using System.Collections;

namespace Maze
{
    public abstract class Algorithm : ScriptableObject
    {
        public abstract int[,] generate(int width, int height, int seed);
    }
}
