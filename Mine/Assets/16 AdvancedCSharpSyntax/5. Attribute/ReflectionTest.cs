using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using SAA = Myproject.SupperAwesomeAttribute;

namespace Myproject
{
    // Reflection : System.Reflection ���ӽ����̽��� ���Ե� ��� ����.
    /*
     ������ Ÿ�ӿ��� ������ Ŭ����, �޼ҵ�, ������� �� ���� ���ؽ�Ʈ�� ���� �����͸�
    �����ϰ� ����ϴ� ���.
    Attribute�� ������ Ÿ�ӿ��� �����Ǵ� ��Ÿ�������̹Ƿ� ���÷����� ���� �����͸� ���� �� �ִ�.

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
            // attTest�� Ÿ���� Ȯ��
            MonoBehaviour attTestBoxingForm = attTest;

            Type attType = attTestBoxingForm.GetType();

            print(attType);

            // attTest��� Ŭ������ �����͸� �������� �ð�
            BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
            // public���� ������ ������ ���ÿ� static�� �ƴ϶� ��ü���� ������ field �Ǵ� property

            // attType : attTest�� GetType�� ���� Ŭ���� ���� ���� �����͸� ������ ����.
            FieldInfo[] fieldInfos = attType.GetFields(bind);

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                SAA attribute = fieldInfo.GetCustomAttribute<SAA>();
                print($"{fieldInfo.Name}�� Ÿ���� {fieldInfo.FieldType}, ��Ʈ����Ʈ ������ {fieldInfo.GetCustomAttributes()}");

                if (attribute is null) {
                    print($"{fieldInfo.Name}�� ���� ������� �ʽ��ϴ�.");
                    continue;
                }
                    

                print($"{fieldInfo.Name}�� ���� ����մϴ�!");
                print($"{attribute.getAwesomeMessage}, {attribute.message}");
                print($"{fieldInfo.GetValue(attTest)}");

            }
        }
    }
}
