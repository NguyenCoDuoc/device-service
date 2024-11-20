using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Validators;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using AutoMapper;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using DeviceService.Domain.Repositories;

namespace DeviceService.Application.Services;

public class AttributeValueService
    : IAttributeValueService
{
    private readonly IAttributeValueRepository _AttributeValueRepository;
    private readonly IMapper _mapper;
    private readonly long current_AttributeValue_id;

    public AttributeValueService(IAttributeValueRepository AttributeValueRepository, IMapper mapper)
    {
        _AttributeValueRepository = AttributeValueRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<AttributeValueDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<AttributeValueDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "value ILIKE @value OR description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("value", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");

        var data = await _AttributeValueRepository.GetPageAsync<AttributeValueDto>(pagingParams.Page, pagingParams.ItemsPerPage,
        order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);

        result.Data = _mapper.Map<List<AttributeValueDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(AttributeValueDtoCreate model)
    {
        model.CreatedBy = current_AttributeValue_id;
        model.CreatedDate = DateTime.Now;

        var validator = new AttributeValueValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _AttributeValueRepository.InsertAsync(_mapper.Map<AttributeValue>(model));

        return new ServiceResultSuccess(_mapper.Map<AttributeValueDto>(data));
    }

    public async Task<AttributeValueDto> UpdateAsync(AttributeValueDto model)
    {
        var entity = _mapper.Map<AttributeValue>(model);
        var data = await _AttributeValueRepository.UpdateAsync(entity);

        return _mapper.Map<AttributeValueDto>(data);
    }

    public async Task<IEnumerable<AttributeValueDtoDetail>> GetAllAsync()
    {
        var data = await _AttributeValueRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AttributeValueDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _AttributeValueRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        AttributeValueDto AttributeValueDto = _mapper.Map<AttributeValueDto>(entity);


        return new ServiceResultSuccess(AttributeValueDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _AttributeValueRepository.DeleteAsync(id, current_AttributeValue_id);
    }
}