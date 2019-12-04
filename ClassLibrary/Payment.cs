using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Payment
    {
        public Payment(int id, User master, string title, string description, int amount, bool isPending, bool isPaid)
        {
            Id = id;
            Master = master;
            Title = title;
            Description = description;
            Amount = amount;
            IsPending = isPending;
            IsPaid = isPaid;
        }
        public int Id { get; }
        public User Master { get; }
        public string Title { get; }
        public string Description { get; }
        public int Amount { get; }
        public bool IsPending { get; }
        public bool IsPaid { get; }
    }
}
