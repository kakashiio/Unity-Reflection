using System;
using System.Collections.Generic;

namespace KLFramework.Reflection
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-24 23:18
    //******************************************
    public interface ITypeContainer
    {
        /// <summary>
        /// Return all types in the container
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetTypes();
    }
}