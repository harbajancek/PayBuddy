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
        private object LoadDataLock { get; set; } = new object();
        FriendsModelView FriendsModelView { get; set; } = new FriendsModelView();
        OutgoingPaymentModelView OutgoingPaymentModelView { get; set; } = new OutgoingPaymentModelView();
        IncomingPaymentModelView IncomingPaymentModelView { get; set; } = new IncomingPaymentModelView();
        User LoggedUser { get; set; } = null;

        public MainWindow()
        {
            InitializeComponent();

            Friends.ItemsSource = FriendsModelView.Friends;
            FriendsSelect.ItemsSource = FriendsModelView.Friends;
            RecievedPayments.ItemsSource = IncomingPaymentModelView.IncomingPayments;
            RecievedPayments.Visibility = Visibility.Visible;
            OutgoingPayments.Visibility = Visibility.Hidden;
        }

        private async void Register_ClickButton(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            registerMessage.Visibility = Visibility.Hidden;
            registerLoading.Visibility = Visibility.Visible;
            string email = registerEmail.Text;
            string password = registerPassword.Password;
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
            string email = loginEmail.Text;
            string password = loginPassword.Password;

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
                loginEmail.Text = string.Empty;
            }

            loginPassword.Password = string.Empty;
            ((Button)sender).IsEnabled = true;
        }

        private async Task LoadData()
        {
            foreach (User user in await DataHandle.GetFriends(LoggedUser))
            {
                FriendsModelView.Friends.Add(user);
            }

            foreach (Payment payment in await DataHandle.GetOwnedPayments(LoggedUser))
            {
                OutgoingPaymentModelView.OutgoingPayments.Add(payment);
            }

            foreach (Payment payment in await DataHandle.GetRecievedPayments(LoggedUser))
            {
                IncomingPaymentModelView.IncomingPayments.Add(payment);
            }
        }

        private void UnloadData()
        {
            LoggedUser = null;

            FriendsModelView.Friends.Clear();
            OutgoingPaymentModelView.OutgoingPayments.Clear();
            IncomingPaymentModelView.IncomingPayments.Clear();
        }

        private async Task ReloadData()
        {
            FriendsModelView.Friends.Clear();
            OutgoingPaymentModelView.OutgoingPayments.Clear();
            IncomingPaymentModelView.IncomingPayments.Clear();
            await LoadData();
        }

        private void Logout_ClickButton(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Hidden;
            LoadingView.Visibility = Visibility.Visible;

            UnloadData();

            LoadingView.Visibility = Visibility.Hidden;
            LoginView.Visibility = Visibility.Visible;
        }

        private async void AddFriend_ClickButton(object sender, RoutedEventArgs e)
        {
            User friend = await DataHandle.GetUserByEmail(AddFriendEmail.Text);
            if (friend == null)
            {
                return;
            }
            await DataHandle.AddFriends(LoggedUser, friend);
            await ReloadData();
        }

        private async void RecievedPayments_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddPaymentView.Visibility = Visibility.Hidden;
            PaymentsView.Visibility = Visibility.Visible;
            RecievedPayments.Visibility = Visibility.Hidden;
            OutgoingPayments.Visibility = Visibility.Hidden;
            PaymentsLabel.Content = "Received Payments";

            await ReloadData();

            RecievedPayments.ItemsSource = IncomingPaymentModelView.IncomingPayments;
            RecievedPayments.Visibility = Visibility.Visible;
        }

        private async void OutgoingPayments_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddPaymentView.Visibility = Visibility.Hidden;
            PaymentsView.Visibility = Visibility.Visible;
            RecievedPayments.Visibility = Visibility.Hidden;
            OutgoingPayments.Visibility = Visibility.Hidden;
            PaymentsLabel.Content = "Outgoing Payments";

            await ReloadData();

            OutgoingPayments.ItemsSource = OutgoingPaymentModelView.OutgoingPayments;
            OutgoingPayments.Visibility = Visibility.Visible;
        }

        private void AddPaymentView_ButtonClick(object sender, RoutedEventArgs e)
        {
            AddPaymentView.Visibility = Visibility.Visible;
            PaymentsView.Visibility = Visibility.Hidden;
        }

        private void Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var item in e.Text)
            {
                if (!char.IsDigit(item))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private async void AddPayment_ButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in FriendsSelect.SelectedItems)
            {
                User friend = (User)item;
                Payment payment = new Payment(0, LoggedUser, friend, Title.Text, Description.Text, int.Parse(Amount.Text), false, false);
                await DataHandle.CreatePayment(payment);
            }

            AddPaymentView.Visibility = Visibility.Hidden;
            PaymentsView.Visibility = Visibility.Visible;
            RecievedPayments.Visibility = Visibility.Hidden;
            OutgoingPayments.Visibility = Visibility.Hidden;

            await ReloadData();

            OutgoingPayments.ItemsSource = OutgoingPaymentModelView.OutgoingPayments;
            OutgoingPayments.Visibility = Visibility.Visible;
        }

        private async void SendDone_ButtonClick(object sender, RoutedEventArgs e)
        {
            await DataHandle.ChangePaymentIsPending((Payment)((Button)sender).DataContext, true);

            await ReloadData();
        }

        private async void ApprovePayment_ButtonClick(object sender, RoutedEventArgs e)
        {
            var payment = (Payment)((Button)sender).DataContext;
            await DataHandle.ChangePaymentIsPaid(payment, true);
            await DataHandle.ChangePaymentIsPending(payment, false);

            await ReloadData();
        }

        private async void RejectPayment_ButtonClick(object sender, RoutedEventArgs e)
        {
            var payment = (Payment)((Button)sender).DataContext;
            await DataHandle.ChangePaymentIsPending(payment, false);

            await ReloadData();
        }
    }
}
