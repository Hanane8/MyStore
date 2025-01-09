using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.ProductsDto
{
    public class AddProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }
        //public Guid CategoryId { get; set; }
    }

}
