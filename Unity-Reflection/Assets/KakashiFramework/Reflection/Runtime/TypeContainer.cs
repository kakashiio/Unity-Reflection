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
    // @Date: 2022-04-24 23:19
    //******************************************
    public class TypeContainer : ITypeContainer
    {
        private Assembly _Assembly;
        
        public TypeContainer(Assembly assembly)
        {
            _Assembly = assembly;
        }
        
        public TypeContainer() : this(Assembly.GetExecutingAssembly())
        {
        }

        public IEnumerable<Type> GetTypes()
        {
            return _Assembly.GetTypes();
        }
    }
}