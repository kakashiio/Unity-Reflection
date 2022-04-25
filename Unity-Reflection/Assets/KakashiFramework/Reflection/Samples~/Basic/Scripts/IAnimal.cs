namespace KakashiFramework.Reflection.Samples.Basic
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-04-25 22:50
    //******************************************
    public interface IAnimal
    {
        void Run();

        [RefMethod]
        void Sleep();

        void Eat();
    }
}