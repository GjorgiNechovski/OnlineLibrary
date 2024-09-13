﻿using OnlineLibrary.Domain.FoodDeliveryEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.FoodDeliveryDomain
{
    public class Dish : BaseEntity
    {
        [Required(ErrorMessage = "The field is required.")]
        public Category? DishCategory { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? DishName { get; set; }


        [Required(ErrorMessage = "The field is required.")]
        public string? DishImage { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? DishIngredients { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public int? Price { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<DishInShoppingCart>? DishInShoppingCarts { get; set; }
        public virtual ICollection<DishInOrder>? DishInOrders { get; set; }


    }
}