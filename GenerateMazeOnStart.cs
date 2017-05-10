using UnityEngine;

namespace Maze
{
    [RequireComponent(typeof(Base))]
    public class GenerateMazeOnStart : MonoBehaviour
    {
        Base mazeGenerator;
        void Start()
        {
            mazeGenerator = GetComponent<Base>();
            mazeGenerator.render();
        }
    }
}