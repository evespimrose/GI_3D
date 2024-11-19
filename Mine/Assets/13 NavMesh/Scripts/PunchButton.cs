using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myproject
{
    [RequireComponent (typeof(Button))]
    public class PunchButton : MonoBehaviour
    {
        private Button button;
        private Tweener punchTween;

        private void Awake()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(Punch);
        }

        private void Punch()
        {
            if (punchTween != null)
            {
                punchTween.Complete();
            }

            Vector3 punchSize = new Vector3(0.1f, 0.1f, 0.1f);
            transform.DOPunchScale(punchSize, 0.5f);

        }
    }
}
