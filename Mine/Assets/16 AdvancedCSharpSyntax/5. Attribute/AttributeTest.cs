using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Myproject
{
    // C#에서의 Attribute는 특정 컨텍스트(클래스 정의, 함수 정의, 변수 선언)에 대한 컴파일 타임에서 주어지는 메타데이터.

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

    // 개발자가 작성한 커스텀 어트리뷰트(system.Attribute를 상속한 클래스) 앞에
    // AttributeUsageAttribute라는 어트리뷰트를 추가해 해당 어트리뷰트의 사용을 제안하거나 추가 설정이 가능.

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
