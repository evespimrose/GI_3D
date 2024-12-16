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

            GameObject obj1 = new("No.1");
            GameObject obj2 = new("no.2");

            SwapObj(obj1 , obj2);
            print($"obj1 : {obj1.name}, obj2 : {obj2.name}");
        }

        private void Swap(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }

        private void SwapObj(GameObject obj1, GameObject obj2)
        {
            (obj2, obj1) = (obj1, obj2);
        }
    }
}
