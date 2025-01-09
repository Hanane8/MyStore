using Application_Layer.DTO.ProductDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ProductCommands.UpdateProductCommands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public UpdateProductDTO ProductDto { get; set; }

        public UpdateProductCommand(Guid id, UpdateProductDTO productDto)
        {
            Id = id;
            ProductDto = productDto;
        }
    }
}
