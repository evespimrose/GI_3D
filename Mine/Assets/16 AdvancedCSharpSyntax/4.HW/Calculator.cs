using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Myproject
{
    public class Calculator : MonoBehaviour
    {
        public TMP_Text text;
        public Button plus;
        public Button minus;
        public Button multiple;
        public Button division;
        public Button result;

        public TMP_InputField left;
        public TMP_InputField right;

        private Action calculate;

        private void Start()
        {
            plus.onClick.AddListener(SetAddition);
            minus.onClick.AddListener(SetSubtraction);
            multiple.onClick.AddListener(SetMultiplication);
            division.onClick.AddListener(SetDivision);

            result.onClick.AddListener(Calc);
        }

        private void SetAddition()
        {
            calculate = () =>
            {
                float leftValue = GetInputValue(left);
                float rightValue = GetInputValue(right);
                text.text = (leftValue + rightValue).ToString();
            };
        }

        private void SetSubtraction()
        {
            calculate = () =>
            {
                float leftValue = GetInputValue(left);
                float rightValue = GetInputValue(right);
                text.text = (leftValue - rightValue).ToString();
            };
        }

        private void SetMultiplication()
        {
            calculate = () =>
            {
                float leftValue = GetInputValue(left);
                float rightValue = GetInputValue(right);
                text.text = (leftValue * rightValue).ToString();
            };
        }

        private void SetDivision()
        {
            calculate = () =>
            {
                float leftValue = GetInputValue(left);
                float rightValue = GetInputValue(right);
                if (Math.Abs(rightValue) > Mathf.Epsilon)
                {
                    text.text = (leftValue / rightValue).ToString();
                }
                else
                {
                    text.text = "Error: Division by Zero";
                }
            };
        }

        private float GetInputValue(TMP_InputField inputField)
        {
            if (float.TryParse(inputField.text, out float value))
            {
                return value;
            }
            else
            {
                text.text = "Error:" + inputField.name + " Invalid Input";
                return 0;
            }
        }

        public void Calc()
        {
            calculate?.Invoke();
        }
    }
}
