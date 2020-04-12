﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using System.Text;
namespace CSharp_PixivGetter_Console
{
    internal class Run
    {
        private int PageCount;
        private int IDCount;
        private object samp = new object();
        private List<string>[] Indexs;
        private Thread[] ts;
        private List<string> indexList = new List<string>();
        private string cookie;
        public Run(string cookie,int threadCount=constr.ThreadCount)
        {
            this.cookie = cookie;
            Start();
        }
        private void Start(int threadCount = constr.ThreadCount)
        {
            ts = new Thread[threadCount];
            IDCount = Init();
            PageCount = IDCount % 20 == 0 ? IDCount / 20 : ((IDCount / 20) + 1);
            int PageCount2 = PageCount - 1;
            Indexs = new List<string>[PageCount];
            for (int i = 0; i < threadCount; i++)
            {
                ts[i] = new Thread(Pross);
            }
            for (int i = 0; i < threadCount; i++)
            {
                ts[i].Start();
            }
            for (int i = 0; i < threadCount; i++)
            {
                ts[i].Join();
            }
            for (; PageCount2 >= 0; PageCount2--)
            {
                indexList.AddRange(Indexs[PageCount2]);
            }
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            RunningPath += "IDCount-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            Console.WriteLine("统计结构存储到 :" + RunningPath);
            FileStream f = File.Create(RunningPath);
            StreamWriter w = new StreamWriter(f,Encoding.UTF8);
            int Count = 1;
            foreach (var s in indexList)
            {
                //Console.WriteLine(Count.ToString() + "---" + s + "\n");
                w.Write(Count++.ToString() + "---" + s + "\n");
                w.Flush();
            }
            w.Close();
            f.Close();


            Console.WriteLine("统计结果共计     "+indexList.Count);
            Console.ReadKey();
        }



        private int Init()
        {
            string str = HttpRequst.GetHtmls(constr.GetAddress(1), "",cookie);
            int i = str.IndexOf("count-badge");
            str = str.Substring(i + 13, 10);
            int LevelCount = 0;
            foreach (var c in str)
            {
                if (c <= 57 && c >= 48)
                    LevelCount++;
                else
                    break;
            }
            try
            {
                return int.Parse(str.Substring(0, LevelCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        private void Pross()
        {
            int i;
            while (true)
            {
                lock (samp)
                {
                    if (PageCount == 0)
                        break;
                    i = PageCount;
                    PageCount--;
                }
                string str = HttpRequst.GetHtmls(constr.GetAddress(i), "", cookie);
                //string str = File.ReadAllText("Z:\\Test" + i.ToString() + ".html");//Debug
                //File.WriteAllText("Z:\\Test" + i.ToString() + ".html", str);//保存下载到的网页数据
                Indexs[i - 1] = HttpRequst.SearchInHtmlFile(str, i);
            }
        }
    }
}
