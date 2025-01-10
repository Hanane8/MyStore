using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeById
{
    public class GetClothingTypeByIdQueryHandler : IRequestHandler<GetClothingTypeByIdQuery, OperationResult<ClothingType>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;

        public GetClothingTypeByIdQueryHandler(IGenericRepository<ClothingType> clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<OperationResult<ClothingType>> Handle(GetClothingTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var clothingType = await _clothingTypeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (clothingType == null)
            {
                return OperationResult<ClothingType>.Failure("ClothingType not found", "Failed to retrieve clothing type.");
            }

            return OperationResult<ClothingType>.Successfull(clothingType, "ClothingType retrieved successfully.");
        }
    }
}
