using UnityEngine;

namespace KakashiFramework.Reflection.Samples.Basic
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-25 22:51
    //******************************************
    public class Dog : IAnimal
    {
        [RefFieldOrProperty]
        private string _Name;

        [RefFieldOrProperty] 
        private int _Age { get; set; }

        public void Run()
        {
            Debug.LogError("Dog is running");
        }

        public void Sleep()
        {
            Debug.LogError("Dog is sleeping");
        }

        public void Eat()
        {
            Debug.LogError("Dog is eating");
        }
    }
}