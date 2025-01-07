﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; } 
        public ICollection<OrderItem>? OrderItems { get; set; }
    }

}
