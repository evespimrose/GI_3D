using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Myproject
{
    // �⺻ �Ű����� : �Ű������� ������ ���� �Ҵ� ���ص� �⺻������ Ư�� ���� ���� �ǵ��� �� �� ����.
    // ��Ÿ���� �ƴ� ������ Ÿ�ӿ��� �� �� �ִ� ���̾�� ��.
    // ��κ� ���ͷ��̶�� ���� ��.
    // [��ȯ��] �Լ��̸�(Ÿ�� �Ķ���� = "�⺻��") {}
    public class DefaultParameter : MonoBehaviour
    {
        public Player newPlayer;
        private void Start()
        {
            GameObject a = CreateNewObject();
            //a.name = "New Object";

            GameObject b = CreateNewObject("New Object2");

            Player player = CreatePlayer("���", 1, 1f, Vector2.zero, new GameObject());

            newPlayer = CreatePlayer("�����", 1,2,3,4,5);

        }

        //private GameObject CreateNewObject()
        //{
        //    return new GameObject();
        //}

        private GameObject CreateNewObject(string name = "Some Object")
        {
            return new GameObject(name);
        }

        // params Ű���� : �Ķ���Ϳ� �迭�� �ް� ���� ���, �� ������ �迭 �Ķ���Ϳ� params Ű���带 �ٿ��θ� �迭 ���� ��� ��ǥ�� �����Ͽ� �迭�� �ڵ� ������ �� �ִ� �Ķ����.

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
