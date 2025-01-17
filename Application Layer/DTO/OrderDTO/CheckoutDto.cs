using Application_Layer.DTO.CartItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.OrderDTO
{
    public class CheckoutDto
    {
        public string? UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? FullName { get; set; }
    }

}
