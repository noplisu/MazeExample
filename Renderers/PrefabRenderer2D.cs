using UnityEngine;
using System.Collections.Generic;

namespace Maze
{
    [CreateAssetMenu(fileName = "PrefabRenderer2D", menuName = "Renderers/PrefabRenderer2D", order = 1)]
    public class PrefabRenderer2D : Renderer
    {
        public float prefabWidth = 1;
        public float prefabHeight = 1;
        public GameObject wall;
        public GameObject air;
        public GameObject enter;
        public GameObject exit;
        public GameObject path;

        public override GameObject render(int[,] maze, List<point> path)
        {
            GameObject root = new GameObject("Maze");
            int mazeWidth = maze.GetLength(0);
            int mazeHeight = maze.GetLength(1);
            float totalMazeWidth = mazeWidth * prefabWidth;
            float totalMazeHeight = mazeHeight * prefabHeight;
            for (int x = 0; x < mazeWidth; ++x)
            {
                float element_x = tilePosition(totalMazeWidth, prefabWidth, x);
                GameObject row = createRow(root, element_x);
                for (int y = 0; y < mazeHeight; ++y)
                {
                    float element_y = tilePosition(totalMazeHeight, prefabHeight, y);
                    GameObject element = elementFactory(element_x, element_y, maze[x, y]);
                    element.transform.parent = row.transform;
                }
                row.transform.parent = root.transform;
            }

            GameObject pathObject = renderPath(path, totalMazeWidth, totalMazeHeight);
            pathObject.transform.parent = root.transform;

            return root;
        }

        private GameObject elementFactory(float x, float y, int elementId)
        {
            GameObject element;
            switch (elementId)
            {
                case 1: { element = createElement(x, y, wall); break; }
                case 2: { element = createElement(x, y, enter); break; }
                case 3: { element = createElement(x, y, exit); break; }
                case 4: { element = createElement(x, y, path); break; }
                default: { element = createElement(x, y, air); break; }
            }
            return element;
        }

        private GameObject createRow(GameObject root, float x_offset)
        {
            GameObject row = new GameObject("Row");
            Vector3 position = root.transform.position;
            position.x = x_offset;
            row.transform.position = position;
            return row;
        }

        private GameObject createElement(float x, float y, GameObject elementPrefab)
        {
            return (GameObject)Instantiate(elementPrefab, new Vector3(x, y, 0), elementPrefab.transform.rotation);
        }

        private float tilePosition(float totalMazeDimension, float tileDimension, int position)
        {
            return -totalMazeDimension / 2 + position * tileDimension;
        }

        private GameObject renderPath(List<point> path, float totalMazeWidth, float totalMazeHeight)
        {
            GameObject pathObject = new GameObject("Path");
            for (int i = 0; i < path.Count; i++)
            {
                GameObject pathNode = new GameObject("Node");
                pathNode.transform.position = new Vector3(
                    tilePosition(totalMazeWidth, prefabWidth, path[i].x),
                    tilePosition(totalMazeHeight, prefabHeight, path[i].y),
                    0
                );
                pathNode.transform.parent = pathObject.transform;
            }
            return pathObject;
        }
    }
}
