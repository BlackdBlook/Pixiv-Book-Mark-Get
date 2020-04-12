using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
namespace CSharp_PixivGetter_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            CreateRun();
        }
        private static void CreateRun()
        {
            string cook;
            Console.WriteLine("输入账号Cookie：\n");
            cook=Console.ReadLine();
            Run r;
            if (cook != "0")
                r = new Run(cook);
            else
                r = new Run();
        }
    }
}