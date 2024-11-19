using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Myproject
{
    public class ExtensionSplit : MonoBehaviour
    {
        public TMP_InputField text;
        private string[] result;

        private void Start()
        {
            text.onEndEdit.AddListener(OnTextEntered);
        }

        private void OnTextEntered(string input)
        {
            result = input.Split(' ');

            foreach (string word in result)
            Debug.Log(word);

            if (result.Length > 0)
            {
                foreach (string word in result)
                Debug.Log($"ToLower: {word.ToLowerExt()}");

                foreach (string word in result)
                    Debug.Log($"ToUpper: {word.ToUpperExt()}");
            }
        }
    }

    // String 클래스 확장 메서드 정의
    public static class StringExtensions
    {
        public static string[] SplitExt(this string input, char separator)
        {
            List<string> result = new();
            string currentWord = string.Empty;

            foreach (char c in input)
            {
                if (c == separator)
                {
                    if (currentWord.Length > 0)
                    {
                        result.Add(currentWord);
                        currentWord = string.Empty; 
                    }
                }
                else
                    currentWord += c;
            }

            if (currentWord.Length > 0)
            {
                result.Add(currentWord);
            }

            return result.ToArray();
        }

        public static string ToLowerExt(this string input)
        {
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c >= 'A' && c <= 'Z')
                result[i] = (char)(c + 32);
                else
                    result[i] = c;
            }

            return new string(result);
        }
        public static string ToUpperExt(this string input)
        {
            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c >= 'a' && c <= 'z')
                    result[i] = (char)(c - 32);
                else
                    result[i] = c;
            }

            return new string(result);
        }
    }
}
