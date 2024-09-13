﻿using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Domain.FoodDeliveryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.FoodDeliveryIdentity
{
    public class FoodDeliveryApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
        public virtual ICollection<Order>? MyOrders { get; set; }


    }
}