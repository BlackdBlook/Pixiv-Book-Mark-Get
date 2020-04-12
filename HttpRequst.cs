using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace CSharp_PixivGetter_Console
{
    internal class HttpRequst
    {
        public static string GetHtmls(string url, string referer = "", string cookie = "", string codeStr = "utf-8")
        {
            var wc = new WebClient { Credentials = CredentialCache.DefaultCredentials };
            try
            {
                var nv = new NameValueCollection {
{"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36"},
{"Content-Type", "application/x-www-form-urlencoded"}
};

                if (referer.Length > 0) { nv.Add("Referer", referer); }
                if (cookie.Length > 0) { nv.Add("Cookie", cookie); }
                wc.Headers.Add(nv);
                Console.WriteLine(url);
                byte[] pageData = wc.DownloadData(url);
                Encoding enc = Encoding.GetEncoding(codeStr);
                return enc.GetString(pageData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + " 获取源代码出错 " + url);
                return "";
            }
            finally { wc.Dispose(); }
        }

        public static void RemoveNChar(ref string str, int N)
        {
            if (N >= str.Length)
                return;
            string ans = "";

            for (int i = N; i < str.Length; i++)
                ans += str[i];
            str = ans;
            return;
        }

        public static List<string> SearchInHtmlFile(string str, int PageIndex)
        {
            List<string> ans = new List<string>();
            int i = 1000;
            int artwork = 1000;
            int next = 0;
            int ix = 0;
            i = str.IndexOf("\"image-item\"", i);
            for (int index = 0; index < 20; index++)
            {
                if (i >= 0)
                {
                    next = str.IndexOf("\"image-item\"", i+300);
                    if(next<0)
                    {
                        artwork = str.IndexOf("/artworks/", i);
                        string ID = str.Substring(artwork + 10, 8);
                        ans.Add(ID);
                        ix++;
                        break;

                    }
                    artwork = str.IndexOf("/artworks/", i);
                    if (artwork<next)
                    {
                        string ID = str.Substring(artwork + 10, 8);
                        ans.Add(ID);
                        ix++;
                    }
                    else
                    {
                        ans.Add("A art work are be deleted ,Pange Index " + PageIndex.ToString());
                        ix++;
                        string ID = str.Substring(artwork + 10, 8);
                        ans.Add(ID);
                        ix++;
                        index++;
                    }
                    i = next;
                }
                else
                {
                    for (; index < 20; index++)
                    { ans.Add("Index:  " + PageIndex.ToString() + "Miss One"); ix++; }
                    break;
                }
            }
            ans.Reverse();
            return ans;
        }
    }
}
