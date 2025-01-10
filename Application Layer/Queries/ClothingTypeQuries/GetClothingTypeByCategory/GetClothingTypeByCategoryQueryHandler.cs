using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeByCategory
{
    public class GetClothingTypeByCategoryQueryHandler : IRequestHandler<GetClothingTypeByCategoryQuery, OperationResult<List<ClothingType>>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;

        public GetClothingTypeByCategoryQueryHandler(IGenericRepository<ClothingType> clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<OperationResult<List<ClothingType>>> Handle(GetClothingTypeByCategoryQuery request, CancellationToken cancellationToken)
        {
            var clothingTypes = await _clothingTypeRepository.FindAsync(
                ct => ct.CategoryId == request.CategoryId,
                cancellationToken
            );

            var clothingTypeList = clothingTypes.ToList();

            if (clothingTypeList == null || !clothingTypeList.Any())
            {
                return OperationResult<List<ClothingType>>.Failure("No clothing types found for this category", "Failed to retrieve clothing types.");
            }

            return OperationResult<List<ClothingType>>.Successfull(clothingTypeList, "Clothing types retrieved successfully.");
        }
    }
}
