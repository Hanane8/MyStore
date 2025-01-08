using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Cart
    {
        public int Id { get; set; } 
        public string? UserId { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public List<CartItem> Items { get; set; } = new List<CartItem>(); 
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice); 
    }
}

