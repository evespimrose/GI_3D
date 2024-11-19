using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myproject
{
    // Delegate 키워드 1: 대리자. 함수의 이름을 대체해줌
    // 내부적으로는 class와 비슷하게 동작

    // delegate의 선언 형태 : [반환형] 델리게이트 이름(파라미터);
    public delegate void SomeMethod(int a);

    public delegate int SomeFunction(int a, int b);

    /*
    Delegate 키워드 2 : 무명 메서드 선언으로 활용.
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

            // 무명 메서드의 단점 : 해당 메서드를 추후에 다시 참조할 수 없음.
            SomeMethod someUnnamedMethod = delegate (int a) { text.text = a.ToString(); };

            someUnnamedMethod?.Invoke(4);

            // 1차 간소화 : delegate 키워드 대신 => 연산자로 대체
            someUnnamedMethod += (int a) => { print(a); };

            // 2차 간소화 : 파라미터 데이터 타입을 생략 가능.

            someUnnamedMethod += (b) => { print(b); text.text = b.ToString(); };
            someUnnamedMethod(4);

            // 3차 간소화 : 함수 내용이 1줄일 경우, 중괄호 생략 가능
            someUnnamedMethod += (c) => print(c);

            // 함수 1줄 간소화의 경우 return 키워드 생략 가능
            SomeFunction someUnnamedFunction = (someIntA,someIntB) => Plus(someIntA,someIntB);

            myMethod += someUnnamedMethod;
            myMethod -= someUnnamedMethod;

            // .netFramework 내장 delegate

            // 1. return이 없는 함수 : Action
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
