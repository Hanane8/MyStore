using Application_Layer.DTO.CartItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.CartDTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? SessionId { get; set; } 
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    }
}
