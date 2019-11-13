using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class Payment
    {
        public User Master { get; }
        public string Title { get; }
        public string Description { get; }
        public int Amount { get; }
        public bool IsPending { get; }
        public bool IsPaid { get; }
    }
}
