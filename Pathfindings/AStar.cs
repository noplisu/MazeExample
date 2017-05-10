using UnityEngine;
using System.Collections.Generic;
using System;

namespace Maze
{
    [CreateAssetMenu(fileName = "AStar", menuName = "Solvers/A*", order = 1)]
    public class AStar : Pathfinding
    {
        List<point> calculatedPath = new List<point>();

        public override int[,] calculate(int[,] map, out List<point> path)
        {
            Vector2 startPosition = LookOnMap(map, 2);
            Vector2 endPosition = LookOnMap(map, 3);
            if(startPosition.x >= 0 && endPosition.x >= 0)
            {
                map = findPath(map, startPosition, endPosition);
            }
            path = calculatedPath;
            return map;
        }

        Vector2 LookOnMap(int[,] map, int value)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == value) { return new Vector2(i, j); }
                }
            }
            return new Vector2(-1, -1);
        }

        List<Node> getNeighbours(int[,] map, Node node)
        {
            List<Node> neighbours = new List<Node>();

            int checkX = (int)node.position.x + 1;
            int checkY = (int)node.position.y;

            if (checkX >= 0 && checkY >= 0 && checkX < map.GetLength(0) && checkY < map.GetLength(1))
            {
                if(map[checkX, checkY] == 0 || map[checkX, checkY] == 3)
                    neighbours.Add(new Node(new Vector2(checkX, checkY)));
            }

            checkX = (int)node.position.x - 1;
            checkY = (int)node.position.y;

            if (checkX >= 0 && checkY >= 0 && checkX < map.GetLength(0) && checkY < map.GetLength(1))
            {
                if (map[checkX, checkY] == 0 || map[checkX, checkY] == 3)
                    neighbours.Add(new Node(new Vector2(checkX, checkY)));
            }

            checkX = (int)node.position.x;
            checkY = (int)node.position.y + 1;

            if (checkX >= 0 && checkY >= 0 && checkX < map.GetLength(0) && checkY < map.GetLength(1))
            {
                if (map[checkX, checkY] == 0 || map[checkX, checkY] == 3)
                    neighbours.Add(new Node(new Vector2(checkX, checkY)));
            }

            checkX = (int)node.position.x;
            checkY = (int)node.position.y - 1;

            if (checkX >= 0 && checkY >= 0 && checkX < map.GetLength(0) && checkY < map.GetLength(1))
            {
                if (map[checkX, checkY] == 0 || map[checkX, checkY] == 3)
                    neighbours.Add(new Node(new Vector2(checkX, checkY)));
            }

            return neighbours;
        }

        int[,] findPath(int[,] map, Vector2 startPosition, Vector2 endPosition)
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(new Node(startPosition));

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i< openSet.Count; i++)
                {
                    if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if(currentNode.position == endPosition)
                {
                    while(currentNode.parent != null)
                    {
                        calculatedPath.Add(new point((int)currentNode.position.x, (int)currentNode.position.y));
                        currentNode = currentNode.parent;
                        if (currentNode.parent != null) map[(int)currentNode.position.x, (int)currentNode.position.y] = 4;
                    }
                    break;
                }

                foreach(Node neighbour in getNeighbours(map, currentNode))
                {
                    if(closedSet.Contains(neighbour)) continue;

                    int newMovementCost = currentNode.gCost + 1;
                    if(newMovementCost < neighbour.gCost  || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = distance(neighbour.position, endPosition);
                        neighbour.parent = currentNode;
                        if(!openSet.Contains(neighbour)) openSet.Add(neighbour);
                    }
                }
            }

            return map;
        }

        int distance(Vector2 A, Vector2 B)
        {
            return (int)(Mathf.Abs(A.x - B.x) + Mathf.Abs(A.y - B.y));
        }

        class Node : IEquatable<Node>
        {
            public Vector2 position;
            public int gCost;
            public int hCost;
            public Node parent;

            public int fCost
            {
                get
                {
                    return gCost + hCost;
                }
            }

            public Node(Vector2 _position)
            {
                position = _position;
            }

            public bool Equals(Node other)
            {
                return position == other.position;
            }
        }
    }
}