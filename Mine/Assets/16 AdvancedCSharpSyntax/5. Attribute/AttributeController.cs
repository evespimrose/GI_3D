using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AttributeController : MonoBehaviour
{
    //Scene�� �ִ� ��� Color ��Ʈ����Ʈ�� ã�Ƽ� ���� �����ִ� ���ҷ� ����� �ʹ�.
    private void Start()
    {
        //Color Attribute�� ���� �ʵ带 ã��
        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>
            (FindObjectsSortMode.None);

        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType(); //Ÿ�� ������ ������.

            //List<FieldInfo> fieldInfos = new List<FieldInfo>(type.GetFields(bind));
            //List<FieldInfo> ColorAttributeAttachedFields =
            //    fieldInfos.FindAll((x) => { return x.HasAttribute<ColorAttribute>(); });

            //����Ʈ �� Collection���� Ž����
            //Linq�� ���� ����ȭ �� ���� ����.
            //1. Linq���� �����ϴ� Ȯ�� �޼��� ���
            IEnumerable<FieldInfo> colorAttachedFields
                = type.GetFields(bind).Where(x => x.HasAttribute<ColorAttribute>());

            IEnumerable<FieldInfo> sizeAttachedFields
                = type.GetFields(bind).Where(x => x.HasAttribute<SizeAttribute>());

            //2. SQL, �������� ����� ���·ε� ����� ����.
            colorAttachedFields = from field in type.GetFields(bind)
                                  where field.HasAttribute<ColorAttribute>()
                                  select field;

            sizeAttachedFields = from field in type.GetFields(bind)
                                  where field.HasAttribute<SizeAttribute>()
                                  select field;

            foreach (FieldInfo fieldInfo in colorAttachedFields)
            {
                ColorAttribute att = fieldInfo.GetCustomAttribute<ColorAttribute>();
                object value = fieldInfo.GetValue(monoBehaviour);

                if (value is Renderer rend)
                    rend.material.color = att.color;
                else if (value is Graphic graph)
                    graph.color = att.color;
                else
                    Debug.LogError("Color ��Ʈ����Ʈ�� �߸��Ȱ��� �پ��ֳ׿�~ ����");
            }

            foreach (FieldInfo fieldInfo in sizeAttachedFields)
            {
                SizeAttribute att = fieldInfo.GetCustomAttribute<SizeAttribute>();
                object value = fieldInfo.GetValue(monoBehaviour);

                if (value is Transform transform)
                    transform.localScale = att.scale;
                else if (value is RectTransform rect)
                    rect.sizeDelta = att.scale;
                else
                    Debug.LogError("Size ��Ʈ����Ʈ�� �߸��Ȱ��� �پ��ֳ׿�~ ����");
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColorAttribute : Attribute
{
    public Color color;
    public ColorAttribute(float r = 0, float g = 0, float b = 0, float a = 1)
    {
        color = new Color(r, g, b, a);
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SizeAttribute : Attribute
{
    public Vector3 scale;
    public SizeAttribute(float x = 1f, float y = 1f, float z = 1f)
    {
        scale = new Vector3(x, y, z);
    }
}

public static class AttributeHelper
{
    public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
    {
        return info.GetCustomAttributes(typeof(T), true).Length > 0;
    }
}
