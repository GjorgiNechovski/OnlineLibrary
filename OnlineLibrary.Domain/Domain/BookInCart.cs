﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Domain
{
    public class BookInCart : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
