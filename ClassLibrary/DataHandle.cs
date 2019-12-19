using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public static async Task<User> GetUserByID(int id)
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_user&id={id}";
            string response = await RequestApi(request);


            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_user>>(text);
                if (array.Count == 0)
                {
                    return null;
                }
                var user = array[0];

                User loggedUser = new User(id: int.Parse(user.id), nick: user.nick, email: user.email);


                return loggedUser;
            }
            return null;
        }

        public static async Task<User> Login(string email, string password)
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_user&email={email}&password={password}";
            string response = await RequestApi(request);
           

            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_user>>(text);
                if (array.Count == 0)
                {
                    return null;
                }
                var user = array[0];

                User loggedUser = new User(id:int.Parse(user.id), nick:user.nick,email:user.email);
                

                return loggedUser;
            }
            return null;
        }

        public static async Task<bool> Register(string email, string password, string nick)
        {
            string request_user = urlAddress + $"?action=show&type=PayBuddy_user&email={email}";

            HttpResponseMessage Response_user = await client.GetAsync(request_user);
            string text = await Response_user.Content.ReadAsStringAsync();
            


            if (Response_user.StatusCode == HttpStatusCode.OK)
            {
                text = text.Substring(2);
                var array = JsonConvert.DeserializeObject<List<PayBuddy_user>>(text);
                if (array.Count != 0)
                {
                    return false;
                }


            }
            else
            {
                return false;
            }

            string request = urlAddress + $"?action=insert&type=PayBuddy_user&email={email}&password={password}&nick={nick}";


            HttpResponseMessage Response_register = await client.GetAsync(request);



            if (Response_register.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
               

                return false;
        }

        public static async Task<IEnumerable<User>> GetFriends(int userId, int id_position = 1)
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_friends&id{id_position}={userId}";
            string response = await RequestApi(request);

            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_friends>>(text);
                if (array.Count == 0)
                {
                    return null;
                }

                List<User> users = new List<User>();

                foreach (PayBuddy_friends friend in array)
                {
                    
                    if(id_position == 1)
                    {
                        users.Add(await GetUserByID(int.Parse(friend.id1)));
                        users.AddRange(await GetFriends(userId, 2));
                    }
                    else
                    {
                        users.Add(await GetUserByID(int.Parse(friend.id2)));
                    }
                    
                }

                return users;

                //return loggedUser;
            }
            return null;


            
            
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
