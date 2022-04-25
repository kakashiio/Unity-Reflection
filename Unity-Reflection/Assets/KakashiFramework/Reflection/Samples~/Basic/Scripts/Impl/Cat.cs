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
    public class Cat : IAnimal
    {
        public void Run()
        {
            Debug.LogError("Cat is running");
        }

        public void Sleep()
        {
            Debug.LogError("Cat is sleeping");
        }

        public void Eat()
        {
            Debug.LogError("Cat is eating");
        }
    }
}