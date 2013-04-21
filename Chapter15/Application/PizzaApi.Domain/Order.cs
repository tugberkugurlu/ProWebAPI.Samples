using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaApi.Domain
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        
        public IEnumerable<OrderItem> Items { get; private set; }

        public decimal TotalPrice { get; set; }

        public string CustomerName { get; set; }

        public int Id { get; set; }
    }
}
