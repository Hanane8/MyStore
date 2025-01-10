using Application_Layer.Commands.ClothingTypeCommands.ClothingTypeCommands;
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

namespace Application_Layer.Commands.ClothingTypeCommands.UpdateClothingTypeCommands
{
    public class UpdateClothingTypeCommandHandler : IRequestHandler<UpdateClothingTypeCommand, OperationResult<Guid>>
    {
        private readonly IGenericRepository<ClothingType> _clothingTypeRepository;
        private readonly IMapper _mapper;

        public UpdateClothingTypeCommandHandler(IGenericRepository<ClothingType> clothingTypeRepository, IMapper mapper)
        {
            _clothingTypeRepository = clothingTypeRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<Guid>> Handle(UpdateClothingTypeCommand request, CancellationToken cancellationToken)
        {
            var clothingTypeDto = request.ClothingTypeDto;

            var existingClothingType = await _clothingTypeRepository.GetByIdAsync(clothingTypeDto.Id, cancellationToken);
            if (existingClothingType == null)
            {
                return OperationResult<Guid>.Failure("ClothingType not found", "Failed to update ClothingType");
            }

            _mapper.Map(clothingTypeDto, existingClothingType);

            _clothingTypeRepository.Update(existingClothingType);
            await _clothingTypeRepository.SaveAsync(cancellationToken);

            return OperationResult<Guid>.Successfull(existingClothingType.Id, "ClothingType updated successfully");
        }
    }
}
