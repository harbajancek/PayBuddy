using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public static class DataHandle
    {
        static HttpClient client = new HttpClient();
        static string urlAddress = "https://hynekma16.sps-prosek.cz/PayBuddy/index.php";
        public static async Task<User> Login(string email, string password)
        {
            string request = urlAddress + "?somethingsomething";
            string response = await RequestApi(request);

            /*
            
            TODO
            somehow get user or when credentials are false return null

            */

            User loggedUser = new User();
            return loggedUser;
        }

        public static async Task<bool> Register(string email, string password, string nick)
        {
            string request = urlAddress + "?somethingsomething";
            string response = await RequestApi(request);

            /*
            
            TODO
            somehow get if registration successfull

            */

            return false;
        }

        public static async Task<IEnumerable<User>> GetFriends(int userId)
        {
            string request = urlAddress + "?somethingsomething";
            string response = await RequestApi(request);

            /*
            
            TODO
            somehow get all friends user

            */

            List<User> users = new List<User>();
            return users;
        }

        public static async Task<IEnumerable<Payment>> GetOwnedPayments(int userID)
        {
            string request = urlAddress + "?somethingsomething";
            string response = await RequestApi(request);

            /*
            
            TODO
            somehow get all payments which the user is master of

            */

            List<Payment> payments = new List<Payment>();
            return payments;
        }

        public static async Task<IEnumerable<Payment>> GetRecievedPayments(int userId)
        {
            string request = urlAddress + "?somethingsomething";
            string response = await RequestApi(request);

            /*
            
            TODO
            somehow get all payments which the user is reciever of

            */

            List<Payment> payments = new List<Payment>();
            return payments;
        }

        private static async Task<string> RequestApi(string uriRequest)
        {
            HttpResponseMessage response = await client.GetAsync(uriRequest);

            return await response.Content.ReadAsStringAsync();
        }
        
    }
}
