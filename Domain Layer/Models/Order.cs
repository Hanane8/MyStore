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
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public DateTime OrderDate { get; set; }
        public  Status OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? FullName { get; set; }

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
