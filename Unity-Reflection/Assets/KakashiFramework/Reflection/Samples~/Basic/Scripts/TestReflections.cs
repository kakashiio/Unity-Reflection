using System;
using System.Reflection;
using KLFramework.Reflection;
using UnityEngine;

namespace KakashiFramework.Reflection.Samples.Basic
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-25 22:52
    //******************************************
    public class TestReflections : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Find all implementations of IAnimal"))
            {
                var types = Reflections.GetTypes(new TypeContainer(Assembly.GetCallingAssembly()), typeof(IAnimal));
                foreach (var type in types)
                {
                    Debug.LogError(type);
                }    
            }
            
            if (GUILayout.Button("Find all methods in IAnimal decorated by RefMethod"))
            {
                var methods = Reflections.GetMethods<RefMethod>(typeof(IAnimal));
                foreach (var method in methods)
                {
                    Debug.LogError(method);
                }    
            }
            
            if (GUILayout.Button("Find all fields or properties in Dog decorated by RefFieldOrProperty"))
            {
                var propertyOrFields = Reflections.GetPropertiesAndFields<RefFieldOrProperty>(typeof(Dog));
                foreach (var propertyOrField in propertyOrFields)
                {
                    Debug.LogError(propertyOrField.Name);
                }    
            }
        }
    }
}