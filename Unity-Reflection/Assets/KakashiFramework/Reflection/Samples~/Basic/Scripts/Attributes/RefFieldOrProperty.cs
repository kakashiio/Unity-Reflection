using System;

namespace KakashiFramework.Reflection.Samples.Basic
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-25 23:00
    //******************************************
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class RefFieldOrProperty : Attribute
    {
    }
}