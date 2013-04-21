using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaApi.Domain;

namespace PizzaApi.Domain
{
    public class SimplePricingService : IPricingService
    {
        public decimal GetPrice(Order order)
        {
            const decimal PriceForAnyPizza = 7.50m;
            const decimal ValueAddedTax = 0.20m;

            var priceBeforeTax = order.Items.Sum(x => x.Quantity)
                                 *PriceForAnyPizza;
            return priceBeforeTax + (priceBeforeTax * ValueAddedTax);
        }
    }
}