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
        public const int ThreadCount = 12;//处理线程数，一定程度上提升带宽利用率和处理速度
        public static string GetAddress(int Count)
        {
            string ans = webAddress;
            ans += "?rest=show&p=";
            ans += Count.ToString();
            return ans;
        }
    }
}
