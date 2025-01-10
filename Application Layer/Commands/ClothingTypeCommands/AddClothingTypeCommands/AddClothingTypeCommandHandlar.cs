using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ClothingTypeCommands.AddClothingTypeCommand
{
    public class AddClothingTypeCommandHandler : IRequestHandler<AddClothingTypeCommand, OperationResult<Guid>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public AddClothingTypeCommandHandler(
            IGenericRepository<ClothingType> clothingTypeRepository,
            IGenericRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _clothingTypeRepository = clothingTypeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<Guid>> Handle(AddClothingTypeCommand request, CancellationToken cancellationToken)
        {
            var clothingType = _mapper.Map<ClothingType>(request.ClothingTypeDto);

            var category = await _categoryRepository.GetByIdAsync(request.ClothingTypeDto.CategoryId, cancellationToken);
            if (category == null)
            {
                return OperationResult<Guid>.Failure("Category not found");
            }

            await _clothingTypeRepository.AddAsync(clothingType, cancellationToken);

            return OperationResult<Guid>.Successfull(clothingType.Id, "ClothingType added successfully");
        }
    }
}
