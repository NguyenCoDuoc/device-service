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

public class DeviceTypeService
    : IDeviceTypeService
{
    private readonly IDeviceTypeRepository _DeviceTypeRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public DeviceTypeService(IDeviceTypeRepository DeviceTypeRepository, IMapper mapper)
    {
        _DeviceTypeRepository = DeviceTypeRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<DeviceTypeDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<DeviceTypeDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "(name ILIKE @name OR code ILIKE @code)";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("code", $"%{pagingParams.Term}%");

        var data = await _DeviceTypeRepository.GetPageAsync<DeviceTypeDto>(pagingParams.Page, pagingParams.ItemsPerPage,
               order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);

        result.Data = _mapper.Map<List<DeviceTypeDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(DeviceTypeDtoCreate model)
    {
        model.CreatedBy = current_user_id;
        model.CreatedDate = DateTime.Now;

        var validator = new DeviceTypeValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _DeviceTypeRepository.InsertAsync(_mapper.Map<DeviceType>(model));

        return new ServiceResultSuccess(_mapper.Map<DeviceTypeDto>(data));
    }

    public async Task<DeviceTypeDto> UpdateAsync(DeviceTypeDto model)
    {
        var entity = _mapper.Map<DeviceType>(model);
        var data = await _DeviceTypeRepository.UpdateAsync(entity);

        return _mapper.Map<DeviceTypeDto>(data);
    }

    public async Task<IEnumerable<DeviceTypeDtoDetail>> GetAllAsync()
    {
        var data = await _DeviceTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DeviceTypeDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _DeviceTypeRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        DeviceTypeDto DeviceTypeDto = _mapper.Map<DeviceTypeDto>(entity);


        return new ServiceResultSuccess(DeviceTypeDto);
    }

    /// <summary>
    /// Get device type attributes by device type id
    /// </summary>
    /// <param name="deviceTypeId">Device type id</param>
    /// <returns>List of device type attributes</returns>
    public async Task<List<DeviceTypeAttributeDto>> GetDeviceTypeAttributes(long deviceTypeId)
    {
        return _mapper.Map<List<DeviceTypeAttributeDto>>(await _DeviceTypeRepository.GetDeviceTypeAttributes(deviceTypeId));
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _DeviceTypeRepository.DeleteAsync(id, current_user_id);
    }

    /// <summary>
    /// Add device type attribute
    /// </summary>
    /// <param name="deviceTypeAttribute">Device type attribute</param>
    /// <returns>Device type attribute id</returns>
    /// DUOCNC 20241106
    public async Task<int> AddDeviceTypeAttribute(DeviceTypeAttributeDto deviceTypeAttribute)
    {
        return await _DeviceTypeRepository.AddDeviceTypeAttribute(_mapper.Map<DeviceTypeAttribute>(deviceTypeAttribute), current_user_id);
    }

    //delete device type attribute
    public async Task<int> DeleteDeviceTypeAttribute(long id)
    {
        return await _DeviceTypeRepository.DeleteDeviceTypeAttribute(id, current_user_id);
    }
}