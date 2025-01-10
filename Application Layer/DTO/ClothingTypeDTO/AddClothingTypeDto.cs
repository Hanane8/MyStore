using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO.ClothingTypeDTO
{
    public class AddClothingTypeDTO
    {
        public string? Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}
