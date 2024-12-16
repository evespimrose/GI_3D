using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Myproject
{
    // C#������ Attribute�� Ư�� ���ؽ�Ʈ(Ŭ���� ����, �Լ� ����, ���� ����)�� ���� ������ Ÿ�ӿ��� �־����� ��Ÿ������.

    public class AttributeTest : MonoBehaviour
    {
        [SerializeField]
        private int imPrivate;

        [TextArea(4,15)]
        public string someText;

        [SupperAwesome("you are not awesome.")]
        public int awesomeInt;

        [SupperAwesome(message : "you are not awesome.", getAwesomeMessage = "not awesome.")]
        public int awesomeInt2;
    }

    [Serializable]
    public class MyColor : ISerializable
    {
        public float red;
        public float green;
        public float blue;
        public float alpha;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }

    // �����ڰ� �ۼ��� Ŀ���� ��Ʈ����Ʈ(system.Attribute�� ����� Ŭ����) �տ�
    // AttributeUsageAttribute��� ��Ʈ����Ʈ�� �߰��� �ش� ��Ʈ����Ʈ�� ����� �����ϰų� �߰� ������ ����.

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public class SupperAwesomeAttribute : Attribute
    {
        public string message;
        public string getAwesomeMessage;

        public SupperAwesomeAttribute()
        {
            message = "I'm Supper Awesome !";
            getAwesomeMessage = "Supper Awesome!";
        }

        public SupperAwesomeAttribute(string message)
        {
            this.message = message;
        }
    }
    
}
