using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddy
{
    class FriendsModelView
    {
        public ObservableCollection<User> Friends { get; set; }
    }
}
