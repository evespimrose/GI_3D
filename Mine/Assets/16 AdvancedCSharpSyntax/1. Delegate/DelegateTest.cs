using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myproject
{
    // Delegate Ű���� 1: �븮��. �Լ��� �̸��� ��ü����
    // ���������δ� class�� ����ϰ� ����

    // delegate�� ���� ���� : [��ȯ��] ��������Ʈ �̸�(�Ķ����);
    public delegate void SomeMethod(int a);

    public delegate int SomeFunction(int a, int b);

    /*
    Delegate Ű���� 2 : ���� �޼��� �������� Ȱ��.
    */



    public class DelegateTest : MonoBehaviour
    {
        public Text text;
        private void Start()
        {
            SomeMethod myMethod = PrintInt;
            myMethod?.Invoke(1);

            myMethod += CreateInt;
            myMethod?.Invoke(2);

            myMethod -= PrintInt;
            myMethod?.Invoke(3);

            myMethod -= CreateInt;
            myMethod?.Invoke(4);

            if(myMethod != null) myMethod.Invoke(5);

            //=========================================================================

            SomeMethod delegateisclass = new SomeMethod(PrintInt);
            delegateisclass(5);

            SomeFunction idontknow = Plus;

            int firstReturn = idontknow(1, 2);
            print(firstReturn);

            idontknow += Multiple;

            int secondReturn = idontknow(2, 3);
            print(secondReturn);

            // ���� �޼����� ���� : �ش� �޼��带 ���Ŀ� �ٽ� ������ �� ����.
            SomeMethod someUnnamedMethod = delegate (int a) { text.text = a.ToString(); };

            someUnnamedMethod?.Invoke(4);

            // 1�� ����ȭ : delegate Ű���� ��� => �����ڷ� ��ü
            someUnnamedMethod += (int a) => { print(a); };

            // 2�� ����ȭ : �Ķ���� ������ Ÿ���� ���� ����.

            someUnnamedMethod += (b) => { print(b); text.text = b.ToString(); };
            someUnnamedMethod(4);

            // 3�� ����ȭ : �Լ� ������ 1���� ���, �߰�ȣ ���� ����
            someUnnamedMethod += (c) => print(c);

            // �Լ� 1�� ����ȭ�� ��� return Ű���� ���� ����
            SomeFunction someUnnamedFunction = (someIntA,someIntB) => Plus(someIntA,someIntB);

            myMethod += someUnnamedMethod;
            myMethod -= someUnnamedMethod;

            // .netFramework ���� delegate

            // 1. return�� ���� �Լ� : Action
            System.Action noneParamMeshod = () => { };
            System.Action<int> intParamMethod = (int a) => { };
            System.Action<string> stringParamMethod = (string b) => { };


        }

        private void PrintInt(int param)
        {
            print(param);
        }

        private void CreateInt(int param)
        {
            new GameObject().name = param.ToString();
        }

        private int Plus(int a, int b)
        {
            return a + b;
        }

        private int Multiple(int a, int b)
        {
            return a * b;
        }
    }
}
