using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class DataHandle
    {
        static HttpClient client = new HttpClient();
        static string urlAddress = "https://hynekma16.sps-prosek.cz/PayBuddy/index.php";
        public static void GerUser(int id,string nick,string email,string password)
        {
            string urlToUse = urlAddress + "?action=show";
            if()
        }

        
    }
}
