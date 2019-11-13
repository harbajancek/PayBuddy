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
        public MainWindow()
        {
            var user = new TestUserClass()
            {
                Nick = "Tom"
            };
            List<TestPayClass> testPays = new List<TestPayClass>()
            {
                new TestPayClass()
                {
                    Master = user,
                    Title = "title",
                    Amount = 100,
                    Description = "descdesc"
                },
                new TestPayClass()
                {
                    Master = user,
                    Title = "title",
                    Amount = 100,
                    Description = "descdesc"
                },
                new TestPayClass()
                {
                    Master = user,
                    Title = "title",
                    Amount = 100,
                    Description = "descdesc"
                },
                new TestPayClass()
                {
                    Master = user,
                    Title = "title",
                    Amount = 100,
                    Description = "descdesc"
                },
                new TestPayClass()
                {
                    Master = user,
                    Title = "title",
                    Amount = 100,
                    Description = "descdesc"
                },
            };
            List<TestUserClass> testUsers = new List<TestUserClass>()
            {
                new TestUserClass()
                {
                    Nick = "pepa"
                },
                new TestUserClass()
                {
                    Nick = "pepa"
                },
                new TestUserClass()
                {
                    Nick = "pepa"
                },
                new TestUserClass()
                {
                    Nick = "pepa"
                },
                new TestUserClass()
                {
                    Nick = "pepa"
                },
            };
            InitializeComponent();

            Friends.ItemsSource = testUsers;
            Payments.ItemsSource = testPays;
        }
    }
}
