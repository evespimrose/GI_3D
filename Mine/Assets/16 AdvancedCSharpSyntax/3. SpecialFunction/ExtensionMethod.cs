using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class ExtensionMethod : MonoBehaviour
    {
        // 특정 요소를 파라미터 대신 .앞에 참조하여 마치 해당 객체가 지닌 메소드인 것 처럼 사용하는 방법

        private void Start()
        {
            StringHelper.StaticMethod();
            string a = StringHelper.Append("나", " 돌아갈래");
            print(a);
            "나".Append(a);

            DateTime totay = DateTime.Now;
            DateTime nextWeek = DateTime.Parse("2024년 11월 25일");

            print(totay.To(nextWeek));
        }
    }
    // static 클래스 : 따로 객체를 생성하지 않아도 data 영역에 변수와 메소드의 정의를 지니고 있는 클래스.
    public static class StringHelper
    {
        public static void StaticMethod()
        {

        }
        
        public static string Append(this string prefix, string postfix)
        {
            return prefix + postfix;
        }
    
    }

    public static class DateTimeHelper
    {
        public static TimeSpan To(this DateTime start, DateTime end)
        {
            return start - end;
        }
    }
    
}
