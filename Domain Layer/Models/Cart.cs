using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Cart
    {
        public Guid Id { get; set; } 
        public string? UserId { get; set; } 
        public User? User { get; set; }
        public DateTime CreatedDate { get; set; } 
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
       
        public Order? Order { get; set; }
    }
}

