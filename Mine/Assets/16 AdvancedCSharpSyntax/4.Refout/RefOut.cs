using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class RefOut : MonoBehaviour
    {
        private void Start()
        {
            int a = 10;
            int b = 20;
            Swap(ref a, ref b);
            print($"a : {a}, b : {b}");
        }

        private void Swap(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }
    }
}
