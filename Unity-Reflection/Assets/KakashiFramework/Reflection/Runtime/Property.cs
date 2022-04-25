using System;
using System.Collections.Generic;
using System.Reflection;

namespace KLFramework.Reflection
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-24 23:17
    //******************************************
    class Property : IPropertyOrField
    {
        private PropertyInfo _PropertyInfo;

        public Property(PropertyInfo prop)
        {
            _PropertyInfo = prop;
        }

        public string Name => _PropertyInfo.Name;

        public void SetValue(object obj, object val)
        {
            _PropertyInfo.SetValue(obj, val);
        }

        public A GetCustomAttribute<A>(bool inherit = false) where A : Attribute
        {
            return _PropertyInfo.GetCustomAttribute<A>(inherit);
        }
        
        public IEnumerable<A> GetCustomAttributes<A>(bool inherit = false) where A : Attribute
        {
            return _PropertyInfo.GetCustomAttributes<A>(inherit);
        }

        public Attribute GetCustomAttribute(Type attributeType, bool inherit = false)
        {
            return _PropertyInfo.GetCustomAttribute(attributeType, inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit = false)
        {
            return _PropertyInfo.GetCustomAttributes(attributeType, inherit);
        }

        public Type GetFieldOrPropertyType()
        {
            return _PropertyInfo.PropertyType;
        }

        public object GetValue(object o)
        {
            return _PropertyInfo.GetValue(o);
        }
    }
}