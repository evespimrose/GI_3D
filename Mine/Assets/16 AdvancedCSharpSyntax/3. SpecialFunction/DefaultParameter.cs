using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Myproject
{
    // 기본 매개변수 : 매개변수에 전달할 값을 할당 안해도 기본적으로 특정 값이 전달 되도록 할 수 있음.
    // 런타임이 아닌 컴파일 타임에서 알 수 있는 값이어야 함.
    // 대부분 리터럴이라고 보면 됨.
    // [반환형] 함수이름(타입 파라미터 = "기본값") {}
    public class DefaultParameter : MonoBehaviour
    {
        public Player newPlayer;
        private void Start()
        {
            GameObject a = CreateNewObject();
            //a.name = "New Object";

            GameObject b = CreateNewObject("New Object2");

            Player player = CreatePlayer("용사", 1, 1f, Vector2.zero, new GameObject());

            newPlayer = CreatePlayer("용사투", 1,2,3,4,5);

        }

        //private GameObject CreateNewObject()
        //{
        //    return new GameObject();
        //}

        private GameObject CreateNewObject(string name = "Some Object")
        {
            return new GameObject(name);
        }

        // params 키워드 : 파라미터에 배열을 받고 싶은 경우, 맨 마지막 배열 파라미터에 params 키워드를 붙여두면 배열 생성 대신 쉼표로 구분하여 배열을 자동 생성할 수 있는 파라미터.

        private Player CreatePlayer(string name, int level = 1, params int[] items)
        {
            Player returnPlayer = CreatePlayer(name, level);
            returnPlayer.items = items;

            return returnPlayer;
        }

        private Player CreatePlayer(string name, int level = 1, float damage = 5f, Vector2 position = default, GameObject obj = null)
        {
            Player returnPlayer = new Player();
            returnPlayer.name = name;
            returnPlayer.level = level;
            returnPlayer.damage = damage;
            returnPlayer.position = position;
            returnPlayer.rendererObject = obj;
            
            return returnPlayer;
        }

        
        [Serializable]
        public class Player
        {
            public string name;
            public int level;
            public float damage;
            public Vector2 position;
            public GameObject rendererObject;
            public int[] items;
        }



    }
}
