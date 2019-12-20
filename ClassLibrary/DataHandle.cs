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


            HttpResponseMessage Response = await client.GetAsync(request);//Budoucí já, promluvuji ti doduše! Oprav to prosím za mě... dole je na toto funkce, nechce se mi to dělat tekže to udělej za mě... děkuji :) ne
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

        public static async Task<User> GetUserByEmail(string email)
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_user&email={email}";
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

        public static async Task<IEnumerable<User>> GetFriends(User user, int id_position = 1)
        {
            int userId = user.Id;

            string request = urlAddress + $"?action=show&type=PayBuddy_friends&id{id_position}={userId}";
            string response = await RequestApi(request);

            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_friends>>(text);
                

                List<User> users = new List<User>();

                foreach (PayBuddy_friends friend in array)
                {
                    
                    if(id_position == 1)
                    {
                        users.Add(await GetUserByID(int.Parse(friend.id2)));
                        
                    }
                    else
                    {
                        users.Add(await GetUserByID(int.Parse(friend.id1)));
                    }
                    
                }
                if (id_position == 1)
                {
                    users.AddRange(await GetFriends(user, 2));
                    return users;


                }
                else
                {
                    return users;
                }
                


            }
            return null;


            
            
        }

        public static async Task<bool> AddFriends(User loggedUser, User friend)
        {
            int id1 = loggedUser.Id;
            int id2 = friend.Id;

            if (id1 > id2)
            {
                int id_temp = id2;
                id2 = id1;
                id1 = id_temp;
            }

            string request_user = urlAddress + $"?action=show&type=PayBuddy_friends&id1={id1}&id2={id2}";

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

            string request = urlAddress + $"?action=insert&type=PayBuddy_friends&id1={id1}&id2={id2}";


            HttpResponseMessage Response_register = await client.GetAsync(request);



            if (Response_register.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }


            return false;


        }

        public static async Task<List<Payment>> GetOwnedPayments(User user_owner) //vše co zadal někomu jinemu
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_payments&id_master={user_owner.Id}";
            string response = await RequestApi(request);

            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_payments>>(text);

                List<Payment> Payments = new List<Payment>();

                foreach (PayBuddy_payments payment in array)
                {
                    Payments.Add(new Payment(id: int.Parse(payment.id), master: await GetUserByID(int.Parse(payment.id_master)), payer: await GetUserByID(int.Parse(payment.id_user)), title: payment.title, description: payment.descr, amount: int.Parse(payment.amount), isPending: bool.Parse(payment.is_pending), isPaid: bool.Parse(payment.is_paid)));
                }
                return Payments; 
            }
            return null;

        }

        public static async Task<List<Payment>> GetRecievedPayments(User user_owner) //vše co má zaplatit
        {
            string request = urlAddress + $"?action=show&type=PayBuddy_payments&id_user={user_owner.Id}";
            string response = await RequestApi(request);

            HttpResponseMessage Response = await client.GetAsync(request);
            string text = await Response.Content.ReadAsStringAsync();
            text = text.Substring(2);


            if (Response.StatusCode == HttpStatusCode.OK)
            {

                var array = JsonConvert.DeserializeObject<List<PayBuddy_payments>>(text);

                List<Payment> Payments = new List<Payment>();

                foreach (PayBuddy_payments payment in array)
                {
                    Payments.Add(new Payment(id: int.Parse(payment.id), master: await GetUserByID(int.Parse(payment.id_master)), payer: await GetUserByID(int.Parse(payment.id_user)), title: payment.title, description: payment.descr, amount: int.Parse(payment.amount), isPending: bool.Parse(payment.is_pending), isPaid: bool.Parse(payment.is_paid)));
                }
                return Payments;
            }
            return null;
        }
        public static async Task<bool> ChangePaymentIsPaid(Payment PaymentToChange, bool ToChange) //změnit paid
        {
            string request = urlAddress + $"?action=update&type=PayBuddy_payments&id_user={PaymentToChange.Id}&is_paid={ToChange}";
            

            HttpResponseMessage Response = await client.GetAsync(request);
            
            if (Response.StatusCode == HttpStatusCode.OK)
            {

                

                return true;
            }
            return false;

        }
        public static async Task<bool> ChangePaymentIsPending(Payment PaymentToChange, bool ToChange)
        {
            string request = urlAddress + $"?action=update&type=PayBuddy_payments&id_user={PaymentToChange.Id}&is_pending={ToChange}";


            HttpResponseMessage Response = await client.GetAsync(request);

            if (Response.StatusCode == HttpStatusCode.OK)
            {



                return true;
            }
            return false;
        }
       
        public static async Task<bool> CreatePayment(Payment PaymentToAdd) ////////////// nwm jestli tam chceš dátt jednotlivé data + uživatele nebo celou platbu, když tak napiš//////////////////////////////////////
        {
            

            string request = urlAddress + $"?action=insert&type=PayBuddy_payments&id_master={PaymentToAdd.Master}&title={PaymentToAdd.Title}&descr={PaymentToAdd.Description}&amount={PaymentToAdd.Amount}&id_user={PaymentToAdd.Payer}&is_paid={PaymentToAdd.IsPaid}&is_pending={PaymentToAdd.IsPending}";


            HttpResponseMessage Response = await client.GetAsync(request);



            if (Response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }


            return false;

            
        }

        private static async Task<string> RequestApi(string uriRequest)
        {
            HttpResponseMessage response = await client.GetAsync(uriRequest);

            return await response.Content.ReadAsStringAsync();
        }
        
    }
}
