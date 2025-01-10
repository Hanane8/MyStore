using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ClothingTypeQuries.GetAllClothingType
{
    public class GetAllClothingTypeQueryHandler : IRequestHandler<GetAllClothingTypeQuery, OperationResult<List<ClothingType>>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;

        public GetAllClothingTypeQueryHandler(IGenericRepository<ClothingType> clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<OperationResult<List<ClothingType>>> Handle(GetAllClothingTypeQuery request, CancellationToken cancellationToken)
        {
            var clothingTypes = await _clothingTypeRepository.GetAllAsync(cancellationToken);

            if (clothingTypes == null || !clothingTypes.Any())
            {
                return OperationResult<List<ClothingType>>.Failure("No clothing types found", "Failed to retrieve clothing types.");
            }

            return OperationResult<List<ClothingType>>.Successfull(clothingTypes.ToList(), "Clothing types retrieved successfully.");
        }
    }
}
