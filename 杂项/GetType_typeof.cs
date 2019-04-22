using System;
using System.Collections.Generic;
using System.Reflection;

namespace Test_MySqlDataDll
{
    public class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(Program);
            //也可通过下面这种方式操作
            //Program obj = new Program();
            //Type t = obj.GetType();
            Console.WriteLine("========Methods====================");

            MethodInfo[] methodInfo = t.GetMethods();
            foreach (MethodInfo mInfo in methodInfo)
                Console.WriteLine(mInfo.ToString());
            Console.WriteLine("========Members====================");
            MemberInfo[] memberInfo = t.GetMembers();
            foreach (MemberInfo mInfo in memberInfo)
            {
                Console.WriteLine(mInfo.ToString());
            }
            Console.ReadKey();
        }
}
