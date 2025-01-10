using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class ClothingType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }

}
