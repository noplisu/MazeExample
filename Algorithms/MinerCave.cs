using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Maze
{
    [CreateAssetMenu(fileName = "MinerCave", menuName = "Algorithms/MinerCave", order = 1)]
    public class MinerCave : Algorithm
    {
        public int newMinerPercent = 8;
        public float maxMinerCountRelativeToMapSize = 0.05f;
        public bool smooth = false;

        public override int[,] generate(int width, int height, int seed)
        {
            Random.InitState(seed);
            int[,]map = new int[width, height];
            int currentMinerCount = 0;
            int minerCount = (int)(width * height * maxMinerCountRelativeToMapSize);


            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = 1;
                }
            }

            List<Miner> miners = new List<Miner>();
            miners.Add(new Miner(1, 1));
            map[1, 0] = 0;
            map[1, 1] = 0;
            currentMinerCount++;

            do
            {
                for(int i = miners.Count - 1; i >= 0; i--)
                {
                    if (miners[i].active)
                    {
                        map = miners[i].dig(map);
                        if(Random.Range(0, 100) <= newMinerPercent && currentMinerCount < minerCount)
                        {
                            Miner newMiner = new Miner(miners[i].x, miners[i].y);
                            map = newMiner.dig(map);
                            miners.Add(newMiner);
                            currentMinerCount++;
                        }
                    }
                    else
                    {
                        if (miners.Count == 1 && currentMinerCount < minerCount)
                            miners[i].move(map);
                        else
                            miners.Remove(miners[i]);
                    }
                }
            } while (miners.Count > 0 || currentMinerCount < minerCount);

            if(smooth)
            {
                for (int i = 1; i < map.GetLength(0) - 2; i++)
                {
                    for (int j = 1; j < map.GetLength(1) - 2; j++)
                    {
                        int neighbours = 0;
                        for(int x = -1; x < 2; x++)
                        {
                            for(int y = -1; y < 2; y++)
                            {
                                if (map[i + x, j + y] == 1)
                                    neighbours++;
                            }
                        }
                        if(neighbours <= 2)
                        {
                            map[i, j] = 0;
                        }
                    }
                }
            }

            for(int i = map.GetLength(0) - 2; i >= 1; i--)
            {
                if(map[i, map.GetLength(1) - 2] == 0)
                {
                    map[i, map.GetLength(1) - 1] = 3;
                    map[1, 0] = 2;
                    return map;
                }
            }

            return map;
        }

        public class Miner
        {
            public int x;
            public int y;
            public bool active = true;

            public Miner(int _x, int _y)
            {
                x = _x;
                y = _y;
            }

            public int[,] dig(int[,] map)
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
                            if (x < map.GetLength(0) - 2 && map[x + 1, y] == 1)
                            {
                                map[x + 1, y] = 0;
                                x++;
                                return map;
                            }
                            break;
                        case 2:
                            if(y < map.GetLength(1) - 2 && map[x, y + 1] == 1)
                            {
                                map[x, y + 1] = 0;
                                y++;
                                return map;
                            }
                            break;
                        case 3:
                            if(x > 1 && map[x - 1, y] == 1)
                            {
                                map[x - 1, y] = 0;
                                x--;
                                return map;
                            }
                            break;
                        case 4:
                            if(y > 1 && map[x, y - 1] == 1)
                            {
                                map[x, y - 1] = 0;
                                y--;
                                return map;
                            }
                            break;
                    }
                }
                active = false;
                return map;
            }

            public void move(int[,] map)
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
                            if (x < map.GetLength(0) - 2)
                            {
                                x++;
                                active = true;
                                return;
                            }
                            break;
                        case 2:
                            if (y < map.GetLength(1) - 2)
                            {
                                y++;
                                active = true;
                                return;
                            }
                            break;
                        case 3:
                            if (x > 1)
                            {
                                x--;
                                active = true;
                                return;
                            }
                            break;
                        case 4:
                            if (y > 1)
                            {
                                y--;
                                active = true;
                                return;
                            }
                            break;
                    }
                }
            }
        }
    }
}