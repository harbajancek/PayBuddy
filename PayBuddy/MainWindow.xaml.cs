using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PayBuddy
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FriendsModelView FriendsModelView { get; set; }
        OutgoingPaymentModelView OutgoingPaymentModelView { get; set; }
        IncomingPaymentModelView IncomingPaymentModelView { get; set; }
        User LoggedUser { get; set; } = null;

        public MainWindow()
        {

            //var user = new TestUserClass()
            //{
            //    Nick = "Tom"
            //};
            //List<TestPayClass> testPays = new List<TestPayClass>()
            //{
            //    new TestPayClass()
            //    {
            //        Master = user,
            //        Title = "title",
            //        Amount = 100,
            //        Description = "descdesc"
            //    },
            //    new TestPayClass()
            //    {
            //        Master = user,
            //        Title = "title",
            //        Amount = 100,
            //        Description = "descdesc"
            //    },
            //    new TestPayClass()
            //    {
            //        Master = user,
            //        Title = "title",
            //        Amount = 100,
            //        Description = "descdesc"
            //    },
            //    new TestPayClass()
            //    {
            //        Master = user,
            //        Title = "title",
            //        Amount = 100,
            //        Description = "descdesc"
            //    },
            //    new TestPayClass()
            //    {
            //        Master = user,
            //        Title = "title",
            //        Amount = 100,
            //        Description = "descdesc"
            //    },
            //};
            //List<TestUserClass> testUsers = new List<TestUserClass>()
            //{
            //    new TestUserClass()
            //    {
            //        Nick = "pepa"
            //    },
            //    new TestUserClass()
            //    {
            //        Nick = "pepa"
            //    },
            //    new TestUserClass()
            //    {
            //        Nick = "pepa"
            //    },
            //    new TestUserClass()
            //    {
            //        Nick = "pepa"
            //    },
            //    new TestUserClass()
            //    {
            //        Nick = "pepa"
            //    },
            //};

            InitializeComponent();

            //Friends.ItemsSource = testUsers;
            //Payments.ItemsSource = testPays;
        }

        private async void Register_ClickButton(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            registerMessage.Visibility = Visibility.Hidden;
            registerLoading.Visibility = Visibility.Visible;
            string email = registerEmail.Text;
            string password = registerPassword.Text;
            string nick = registerNick.Text;

            bool success = await DataHandle.Register(email, password, nick);
            registerLoading.Visibility = Visibility.Hidden;

            if (success)
            {
                registerMessage.Content = "Registration successfull";
                registerMessage.Foreground = Brushes.Black;
            }
            else
            {
                registerMessage.Content = "Registration unsuccessfull";
                registerMessage.Foreground = Brushes.Red;
            }

            registerMessage.Visibility = Visibility.Visible;

            ((Button)sender).IsEnabled = true;
        }

        private async void Login_ClickButton(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            loginMessage.Visibility = Visibility.Hidden;
            loginLoading.Visibility = Visibility.Visible;
            string email = registerEmail.Text;
            string password = registerPassword.Text;

            LoggedUser = await DataHandle.Login(email, password);
            loginLoading.Visibility = Visibility.Hidden;

            if (LoggedUser == null)
            {
                loginMessage.Visibility = Visibility.Visible;
                loginMessage.Foreground = Brushes.Red;
            }
            else
            {
                LoginView.Visibility = Visibility.Hidden;
                LoadingView.Visibility = Visibility.Visible;
                await LoadData();
                LoadingView.Visibility = Visibility.Hidden;
                MainView.Visibility = Visibility.Visible;
            }

            ((Button)sender).IsEnabled = true;
        }

        private async Task LoadData()
        {
            foreach (var item in await DataHandle.GetFriends(LoggedUser.Id))
            {
                FriendsModelView.Friends.Add(item);
            }

            foreach (var item in await DataHandle.GetOwnedPayments(LoggedUser.Id))
            {
                OutgoingPaymentModelView.OutgoingPayments.Add(item);
            }

            foreach (var item in await DataHandle.GetRecievedPayments(LoggedUser.Id))
            {
                IncomingPaymentModelView.IncomingPayments.Add(item);
            }
        }
    }
}
