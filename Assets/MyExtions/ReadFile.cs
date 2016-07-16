using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;

public class ReadFile {

    public static List<FieldInfo> GetFields(string classname)
    {
        BindingFlags flags = BindingFlags.Instance |
                                 BindingFlags.Static |
                                 BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.FlattenHierarchy;
        List<FieldInfo> fields = new List<FieldInfo>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.FullName == classname)
                {

                    foreach (FieldInfo field in type.GetFields(flags))
                    {
                        fields.Add(field);
                    }
                }
            }
        }
        return fields;
    }

    public static List<MethodInfo> GetMethods(string classname)
    {
        BindingFlags flags = BindingFlags.Instance |
                                 BindingFlags.Static |
                                 BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.FlattenHierarchy;
        List<MethodInfo> methods = new List<MethodInfo>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.FullName == classname)
                {

                    foreach (MethodInfo method in type.GetMethods(flags))
                    {
                        methods.Add(method);
                    }
                }
            }
        }
        return methods;
    }

    public static List<PropertyInfo> GetPropeties(string classname)
    {
        BindingFlags flags = BindingFlags.Instance |
                                 BindingFlags.Static |
                                 BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.FlattenHierarchy;
        List<PropertyInfo> properties = new List<PropertyInfo>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.FullName == classname)
                {

                    foreach (PropertyInfo property in type.GetProperties(flags))
                    {
                        properties.Add(property);
                    }
                }
            }
        }
        return properties;
    }
}
