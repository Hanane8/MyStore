using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.CartDTO
{
    public class AddToCartDTO
    {
        public Guid ProductId { get; set; }
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
