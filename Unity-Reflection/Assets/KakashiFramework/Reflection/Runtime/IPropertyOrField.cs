using System;
using System.Collections.Generic;

namespace KLFramework.Reflection
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-24 23:16
    //******************************************
    public interface IPropertyOrField
    {
        /// <summary>
        /// Property or field name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Set property or field value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        void SetValue(object obj, object val);

        /// <summary>
        /// Get custom attribute on property or field
        /// </summary>
        /// <param name="inherit"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        A GetCustomAttribute<A>(bool inherit = false) where A : Attribute;
        
        /// <summary>
        /// Get custom attributes on property or field
        /// </summary>
        /// <param name="inherit"></param>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        IEnumerable<A> GetCustomAttributes<A>(bool inherit = false) where A : Attribute;
        
        /// <summary>
        /// Get custom attribute on property or field
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        Attribute GetCustomAttribute(Type attributeType, bool inherit = false);
        
        /// <summary>
        /// Get custom attributes on property or field
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        object[] GetCustomAttributes(Type attributeType, bool inherit = false);
        
        /// <summary>
        /// Get the field or property type
        /// </summary>
        /// <returns></returns>
        Type GetFieldOrPropertyType();
        
        /// <summary>
        /// Get the field or property value
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        object GetValue(object o);
    }
}