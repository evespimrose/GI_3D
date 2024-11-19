using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class Parameter : MonoBehaviour
    {
        private void Start()
        {
            PrintLines("Hello, World!", "Welcome to Unity", "This is a test.");
        }
        public void PrintLines(params string[] lines)
        {
            foreach (var line in lines)
            {
                Debug.Log(line);
            }
        }
    }
}
