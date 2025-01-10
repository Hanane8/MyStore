using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ClothingTypeCommands.DeleteClothingTypeCommands
{
    public class DeleteClothingTypeCommandHandler : IRequestHandler<DeleteClothingTypeCommand, OperationResult<Guid>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;

        public DeleteClothingTypeCommandHandler(IGenericRepository<ClothingType> clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<OperationResult<Guid>> Handle(DeleteClothingTypeCommand request, CancellationToken cancellationToken)
        {
            var clothingType = await _clothingTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (clothingType == null)
            {
                return OperationResult<Guid>.Failure("ClothingType not found", "Failed to delete ClothingType");
            }

            _clothingTypeRepository.Remove(clothingType);
            await _clothingTypeRepository.SaveAsync(cancellationToken);  

            return OperationResult<Guid>.Successfull(clothingType.Id, "ClothingType deleted successfully");
        }
    }
}
