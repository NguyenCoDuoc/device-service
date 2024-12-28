using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Validators;
using DeviceService.Domain.Interfaces;
using AutoMapper;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using Attribute = DeviceService.Domain.Entities.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Application.Services;

public class AttributeService
    : IAttributeService
{
    private readonly IAttributeRepository _AttributeRepository;
    private readonly IMapper _mapper;
    private readonly long current_Attribute_id;

    public AttributeService(IAttributeRepository AttributeRepository, IMapper mapper)
    {
        _AttributeRepository = AttributeRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<AttributeDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<AttributeDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR description ILIKE @description OR code ILIKE @code";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");
        param.Add("code", $"%{pagingParams.Term}%");
        var data = await _AttributeRepository.GetPageAsync<AttributeDto>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<AttributeDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(AttributeDtoCreate model)
    {
        model.CreatedBy = current_Attribute_id;
        model.CreatedDate = DateTime.Now;

        var validator = new AttributeValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _AttributeRepository.InsertAsync(_mapper.Map<Attribute>(model));

        return new ServiceResultSuccess(_mapper.Map<AttributeDto>(data));
    }

    public async Task<AttributeDto> UpdateAsync(AttributeDto model)
    {
        var entity = _mapper.Map<Attribute>(model);
        var data = await _AttributeRepository.UpdateAsync(entity);

        return _mapper.Map<AttributeDto>(data);
    }

    public async Task<IEnumerable<AttributeDtoDetail>> GetAllAsync()
    {
        var data = await _AttributeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AttributeDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _AttributeRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        AttributeDto AttributeDto = _mapper.Map<AttributeDto>(entity);


        return new ServiceResultSuccess(AttributeDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _AttributeRepository.DeleteAsync(id, current_Attribute_id);
    }
}