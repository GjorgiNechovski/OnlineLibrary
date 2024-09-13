using OnlineLibrary.Domain.FoodDeliveryIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.FoodDeliveryDomain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public FoodDeliveryApplicationUser? Owner { get; set; }
        public virtual ICollection<DishInShoppingCart>? DishInShoppingCarts { get; set; }

    }
}
