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
            if(!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Cookies.txt"))
            {
                CreateCookieFile();
                Console.ReadKey();
                return;
            }
            else
            {
                string s = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Cookies.txt");
                if (s.Length > 0)
                    CreateRun(s);
                else
                    CreateCookieFile();
            }            
            Console.ReadKey();
        }
        private static void CreateCookieFile()
        {
            File.Create(AppDomain.CurrentDomain.BaseDirectory + "Cookies.txt"); 
            Console.WriteLine("在  "+ AppDomain.CurrentDomain.BaseDirectory + "Cookies.txt  中输入账号的Cookies");
        }
        private static void CreateRun(string cook)
        {
            _ = new Run(cook);
        }
    }
}