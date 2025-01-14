using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public  Status OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }

        public void SetTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.TotalPrice);
        }



        public enum Status
        {
            Pending,
            Shipped,
            Delivered
        }
    }

}
