using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class Pointer : MonoBehaviour
    {
        public LayerMask targetLayer;
        private Renderer childRenderer;

        private void Awake()
        {
            childRenderer = GetComponentInChildren<Renderer>();
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, layerMask : targetLayer, maxDistance : 1000f))
                {
                    transform.position = hit.point;
                    transform.GetChild(0).DOLocalJump(Vector3.zero, 3f, 2, 0.5f)
                    .OnStart(() => childRenderer.enabled = true)
                    .OnComplete(() => childRenderer.enabled = false);
                }
            }
        }
    }
}
