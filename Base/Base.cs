using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    public class Base : MonoBehaviour
    {
        public int w = 11;
        public int h = 11;
        public int seed = 1;
        public bool useRandomSeed = true;
        public Algorithm algorithm;
        public Pathfinding solver;
        public Renderer mazeRenderer;
        public bool showPath = true;

        int[,] maze;
        GameObject mazeRoot;
        GameObject follow;
        List<point> path;

        public void render()
        {
            clearOldMaze();
            randomizeSeed();
            maze = generateMaze();
            renderMaze(maze);
        }
        
        public List<point> getPath() { return path; }

        private void clearOldMaze() { if (mazeRoot) { Destroy(mazeRoot); } }
        private void randomizeSeed() { if (useRandomSeed) { seed = Random.Range(int.MinValue, int.MaxValue); } }

        private int[,] generateMaze() {
            int[,] maze = algorithm.generate(w, h, seed);
            if (solver != null && showPath) { maze = solver.calculate(maze, out path); }
            return maze;
        }

        private void renderMaze(int[,] maze)
        {
            mazeRoot = mazeRenderer.render(maze, path);
            mazeRoot.transform.SetParent(transform, false);
        }
    }
}