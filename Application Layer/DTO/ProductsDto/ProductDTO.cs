using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.ProductsDto
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Size { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }  
    }
}
