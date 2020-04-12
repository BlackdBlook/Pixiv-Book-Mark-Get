using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_PixivGetter_Console
{
    class constr
    {
        public const string webAddress = "https://www.pixiv.net/bookmark.php";
        public const int ThreadCount = 12;
        public static string GetAddress(int Count)
        {
            string ans = webAddress;
            ans += "?rest=show&p=";
            ans += Count.ToString();
            return ans;
        }
    }
}
