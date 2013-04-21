using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaApi.Domain;

namespace PizzaApi.Api.Domain
{
    public interface IPricingService
    {
        decimal GetPrice(Order order);
    }
}
