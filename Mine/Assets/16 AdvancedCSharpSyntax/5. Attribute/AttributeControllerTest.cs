using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeControllerTest : MonoBehaviour
{
    [Color(0, 1, 0, 1)]
    public Renderer rend;

    //[SerializeField, Color(r: 1, b: 0.5f)]
    //public Graphic graph;

    //[Color]
    //public float notRendererOrGraphic;

    [Size(0.75f, 0.5f, 0.75f)]
    public Transform Scale;

}
