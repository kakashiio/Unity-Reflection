using System;
using System.Collections.Generic;

namespace KLFramework.Reflection
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-24 23:19
    //******************************************
    public class TypeContainerCollection : ITypeContainer
    {
        private IEnumerable<ITypeContainer> _TypeHolders;
        
        public TypeContainerCollection(IEnumerable<ITypeContainer> typeHolders)
        {
            _TypeHolders = typeHolders;
        }

        public IEnumerable<Type> GetTypes()
        {
            var types = new HashSet<Type>();
            foreach (var typeHolder in _TypeHolders)
            {
                var ts = typeHolder.GetTypes();
                if (ts == null)
                {
                    continue;
                }

                foreach (var t in ts)
                {
                    types.Add(t);
                }
            }

            return types;
        }
    }
}