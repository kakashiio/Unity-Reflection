using System;
using System.Collections.Generic;
using System.Reflection;

namespace KLFramework.Reflection
{
    //******************************************
    // Util class for reflection  
    // 
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-24 23:12
    //******************************************
    public class Reflections
    {
        public enum TargetType
        {
            CLASS = 1 << 0,
            STRUCT = 1 << 1,
            ATTRIBUTE = 1 << 2,
            CLASS_OR_STRUCT = CLASS | STRUCT,
            ALL = CLASS | STRUCT | ATTRIBUTE,
            DEFAULT = CLASS_OR_STRUCT
        }

        private const BindingFlags DEFAULT_BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        private static readonly Type TYPE_ATTRIBUTE = typeof(Attribute);        
        private static readonly ITypeContainer DefaultTypeContainer = new TypeContainer();
        
        /// <summary>
        /// Find all types which implement or extends from the type `baseOrInterface`
        /// </summary>
        /// <param name="typeContainer"></param>
        /// <param name="baseOrInterface"></param>
        /// <param name="allowAbstract"></param>
        /// <returns></returns>
        public static List<Type> GetTypes(ITypeContainer typeContainer, Type baseOrInterface, bool allowAbstract = false)
        {
            var types = typeContainer.GetTypes();
                     
            List<Type> validTypes = new List<Type>();
            foreach (var type in types)
            {
                if (baseOrInterface.IsAssignableFrom(type) && (allowAbstract || !type.IsAbstract))
                {
                    validTypes.Add(type);
                }
            }

            return validTypes;
        }
        
        /// <summary>
        /// Find all types which implement or extends from the type `baseOrInterface`.
        /// </summary>
        /// <param name="baseOrInterface"></param>
        /// <param name="allowAbstract"></param>
        /// <returns></returns>
        public static List<Type> GetTypes(Type baseOrInterface, bool allowAbstract = false)
        {
            return GetTypes(DefaultTypeContainer, baseOrInterface, allowAbstract);
        }

        /// <summary>
        /// Find all types which is decorating with `attributeTypes` Attributes
        /// </summary>
        /// <param name="typeContainer"></param>
        /// <param name="attributeTypes"></param>
        /// <param name="targetType">Which kind of type you want to find</param>
        /// <returns></returns>
        public static List<Type> GetTypesWithAttributes(ITypeContainer typeContainer, IEnumerable<Type> attributeTypes, TargetType targetType = TargetType.DEFAULT)
        {
            var types = typeContainer.GetTypes();
            List<Type> list = new List<Type>();
            foreach (var type in types)
            {
                if ((type.IsClass && (targetType & TargetType.CLASS) == 0)
                    || (IsAttribute(type) && (targetType & TargetType.ATTRIBUTE) == 0)
                    || (IsStruct(type) && (targetType & TargetType.STRUCT) == 0))
                {
                    continue;
                }

                foreach (var attributeType in attributeTypes)
                {
                    if (type.GetCustomAttribute(attributeType) != null)
                    {
                        list.Add(type);  
                        break;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Find all types which is decorating with `attributeTypes` Attributes
        /// </summary>
        /// <param name="typeContainer"></param>
        /// <param name="attributeType"></param>
        /// <param name="targetType">Which kind of type you want to find</param>
        /// <returns></returns>
        public static List<Type> GetTypesWithAttributes(ITypeContainer typeContainer, Type attributeType, TargetType targetType = TargetType.DEFAULT)
        {
            return GetTypesWithAttributes(typeContainer, new [] { attributeType }, targetType);
        }

        /// <summary>
        /// Get all fields which is decorating with `A` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<FieldInfo> GetFields<A>(object obj, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetFields<A>(obj.GetType(), bindingFlags);
        }
        
        /// <summary>
        /// Get all fields which is decorating with `attribute` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<FieldInfo> GetFields(object obj, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) 
        {
            return GetFields(obj.GetType(), attribute, bindingFlags);
        }

        /// <summary>
        /// Get all fields which is decorating with `A` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<FieldInfo> GetFields<A>(Type type, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetFields(type, typeof(A), bindingFlags);
        }
        
        /// <summary>
        /// Get all fields which is decorating with `attribute` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<FieldInfo> GetFields(Type type, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) 
        {
            var fields = type.GetFields(bindingFlags);

            List<FieldInfo> fieldInfos = new List<FieldInfo>();

            for (int i = 0; i < fields.Length; i++)
            {
                if (attribute == null || fields[i].GetCustomAttribute(attribute) != null)
                {
                    fieldInfos.Add(fields[i]);
                }
            }

            return fieldInfos;
        }
        
        /// <summary>
        /// Get all properties which is decorating with `A` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<PropertyInfo> GetProperties<A>(Type type, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetProperties(type, typeof(A), bindingFlags);
        }
        
        /// <summary>
        /// Get all properties which is decorating with `attribute` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetProperties(Type type, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) 
        {
            var fields = type.GetProperties(bindingFlags);

            List<PropertyInfo> fieldInfos = new List<PropertyInfo>();

            for (int i = 0; i < fields.Length; i++)
            {
                if (attribute == null || fields[i].GetCustomAttribute(attribute) != null)
                {
                    fieldInfos.Add(fields[i]);
                }
            }

            return fieldInfos;
        }

        /// <summary>
        /// Get all properties which is decorating with `A` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<PropertyInfo> GetProperties<A>(object obj, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetProperties<A>(obj.GetType(), bindingFlags);
        }
        
        /// <summary>
        /// Get all properties which is decorating with `attribute` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetProperties(object obj, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) 
        {
            return GetProperties(obj.GetType(), attribute, bindingFlags);
        }

        /// <summary>
        /// Get all properties and fields which is decorating with `A` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<IPropertyOrField> GetPropertiesAndFields<A>(object obj, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetPropertiesAndFields<A>(obj.GetType(), bindingFlags);
        }
        
        /// <summary>
        /// Get all properties and fields which is decorating with `attribute` Attribute from `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<IPropertyOrField> GetPropertiesAndFields(object obj, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS)
        {
            return GetPropertiesAndFields(obj.GetType(), attribute, bindingFlags);
        }

        /// <summary>
        /// Get all properties and fields which is decorating with `A` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static List<IPropertyOrField> GetPropertiesAndFields<A>(Type type, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetPropertiesAndFields(type, typeof(A), bindingFlags);
        }
        
        /// <summary>
        /// Get all properties and fields which is decorating with `attribute` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<IPropertyOrField> GetPropertiesAndFields(Type type, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS)
        {
            List<IPropertyOrField> list = new List<IPropertyOrField>();

            foreach (var prop in GetProperties(type, attribute, bindingFlags))
            {
                list.Add(new Property(prop));
            }
            
            foreach (var field in GetFields(type, attribute, bindingFlags))
            {
                list.Add(new Field(field));
            }

            return list;
        }

        /// <summary>
        /// Get all methods which is decorating with `A` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<MethodInfo> GetMethods<A>(Type type, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) where A : Attribute
        {
            return GetMethods(type, typeof(A), bindingFlags);
        }

        /// <summary>
        /// Get all methods which is decorating with `attribute` Attribute from `type`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethods(Type type, Type attribute, BindingFlags bindingFlags = DEFAULT_BINDING_FLAGS) 
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            
            var methods = type.GetMethods(bindingFlags);
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute(attribute) != null)
                {
                    methodInfos.Add(method);    
                }
            }
            
            return methodInfos;
        }

        /// <summary>
        /// Check whether the given `type` is an Attribute Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAttribute(Type type)
        {
            return TYPE_ATTRIBUTE.IsAssignableFrom(type);
        }

        /// <summary>
        /// Check whether the given `type` is a Struct Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStruct(Type type)
        {
            return type.IsValueType && !type.IsEnum &&
                           !type.IsEquivalentTo(typeof(decimal)) && 
                           !type.IsPrimitive;
        }
    }
}