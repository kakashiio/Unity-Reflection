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
    public class Field : IPropertyOrField
    {
        private FieldInfo _FieldInfo;

        public Field(FieldInfo field)
        {
            _FieldInfo = field;
        }

        public string Name => _FieldInfo.Name;

        public void SetValue(object obj, object val)
        {
            _FieldInfo.SetValue(obj, val);
        }

        public A GetCustomAttribute<A>(bool inherit = false) where A : Attribute
        {
            return _FieldInfo.GetCustomAttribute<A>(inherit);
        }
        
        public IEnumerable<A> GetCustomAttributes<A>(bool inherit = false) where A : Attribute
        {
            return _FieldInfo.GetCustomAttributes<A>(inherit);
        }

        public Attribute GetCustomAttribute(Type attributeType, bool inherit = false)
        {
            return _FieldInfo.GetCustomAttribute(attributeType, inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit = false)
        {
            return _FieldInfo.GetCustomAttributes(attributeType, inherit);
        }

        public Type GetFieldOrPropertyType()
        {
            return _FieldInfo.FieldType;
        }

        public object GetValue(object o)
        {
            return _FieldInfo.GetValue(o);
        }
    }
}