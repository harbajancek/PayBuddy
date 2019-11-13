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
            List<TestClass> tests = new List<TestClass>()
            {
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                },
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                },
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                },
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                },
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                },
                new TestClass()
                {
                    Title = "kino",
                    Amount = "199 Kč",
                    Description = "do kina",
                    Name = "Pepa"
                }
            };

            InitializeComponent();

            Payments.ItemsSource = tests;
        }
    }
}
