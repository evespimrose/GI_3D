using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myproject
{
    public class ExtensionMethod : MonoBehaviour
    {
        // Ư�� ��Ҹ� �Ķ���� ��� .�տ� �����Ͽ� ��ġ �ش� ��ü�� ���� �޼ҵ��� �� ó�� ����ϴ� ���

        private void Start()
        {
            StringHelper.StaticMethod();
            string a = StringHelper.Append("��", " ���ư���");
            print(a);
            "��".Append(a);

            DateTime totay = DateTime.Now;
            DateTime nextWeek = DateTime.Parse("2024�� 11�� 25��");

            print(totay.To(nextWeek));
        }
    }
    // static Ŭ���� : ���� ��ü�� �������� �ʾƵ� data ������ ������ �޼ҵ��� ���Ǹ� ���ϰ� �ִ� Ŭ����.
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
