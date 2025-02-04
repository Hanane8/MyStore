using Application_Layer.DTO.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public string? Status { get; set; }  
        public List<OrderItemDTO>? Items { get; set; }
        public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity); 
    }

}
