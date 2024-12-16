using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using SAA = Myproject.SupperAwesomeAttribute;

namespace Myproject
{
    // Reflection : System.Reflection 네임스페이스에 포함된 기능 전반.
    /*
     컴파일 타임에서 생성된 클래스, 메소드, 멤버변수 등 여러 컨텍스트에 대한 데이터를
    색인하고 취급하는 기능.
    Attribute는 컴파일 타임에서 생성되는 메타데이터이므로 리플렉션을 통해 데이터를 가질 수 있다.

     */

    [RequireComponent (typeof(AttributeTest))]
    public class ReflectionTest : MonoBehaviour
    {
        AttributeTest attTest;

        private void Awake()
        {
            attTest = GetComponent<AttributeTest> ();
        }

        private void Start()
        {
            // attTest의 타입을 확인
            MonoBehaviour attTestBoxingForm = attTest;

            Type attType = attTestBoxingForm.GetType();

            print(attType);

            // attTest라는 클래스의 데이터를 따져보는 시간
            BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
            // public으로 접근이 가능한 동시에 static이 아니라 객체별로 생성할 field 또는 property

            // attType : attTest의 GetType를 통해 클래스 명세에 대한 데이터를 가지고 있음.
            FieldInfo[] fieldInfos = attType.GetFields(bind);

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                SAA attribute = fieldInfo.GetCustomAttribute<SAA>();
                print($"{fieldInfo.Name}의 타입은 {fieldInfo.FieldType}, 어트리뷰트 개수는 {fieldInfo.GetCustomAttributes()}");

                if (attribute is null) {
                    print($"{fieldInfo.Name}는 슈퍼 어썸하지 않습니다.");
                    continue;
                }
                    

                print($"{fieldInfo.Name}는 슈퍼 어썸합니다!");
                print($"{attribute.getAwesomeMessage}, {attribute.message}");
                print($"{fieldInfo.GetValue(attTest)}");

            }
        }
    }
}
