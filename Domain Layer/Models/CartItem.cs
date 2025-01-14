﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid? CartId { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; private set; }

        public void SetTotalPrice()
        {
            TotalPrice = UnitPrice * Quantity;
        }

    }

}
