﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public enum Status { Pending, Shipped, Delivered }
        public decimal TotalPrice => OrderItems.Sum(item => item.TotalPrice); 
        public ICollection<OrderItem>? OrderItems { get; set; }
    }

}
