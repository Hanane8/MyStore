using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.CartItemDTO
{
    public class UpdateCartItemDTO
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; } = string.Empty;
    }
}
