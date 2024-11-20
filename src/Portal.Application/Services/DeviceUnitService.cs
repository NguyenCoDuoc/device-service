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

public class DeviceUnitService
    : IDeviceUnitService
{
    private readonly IDeviceUnitRepository _DeviceUnitRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public DeviceUnitService(IDeviceUnitRepository DeviceUnitRepository, IMapper mapper)
    {
        _DeviceUnitRepository = DeviceUnitRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<DeviceUnitDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<DeviceUnitDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR code ILIKE @code";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("code", $"%{pagingParams.Term}%");

        var data = await _DeviceUnitRepository.GetPageAsync<DeviceUnitDto>(pagingParams.Page, pagingParams.ItemsPerPage,
               order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<DeviceUnitDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(DeviceUnitDtoCreate model)
    {
        model.CreatedBy = current_user_id;
        model.CreatedDate = DateTime.Now;

        var validator = new DeviceUnitValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _DeviceUnitRepository.InsertAsync(_mapper.Map<DeviceUnit>(model));

        return new ServiceResultSuccess(_mapper.Map<DeviceUnitDto>(data));
    }

    public async Task<DeviceUnitDto> UpdateAsync(DeviceUnitDto model)
    {
        var entity = _mapper.Map<DeviceUnit>(model);
        var data = await _DeviceUnitRepository.UpdateAsync(entity);

        return _mapper.Map<DeviceUnitDto>(data);
    }

    public async Task<IEnumerable<DeviceUnitDtoDetail>> GetAllAsync()
    {
        var data = await _DeviceUnitRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DeviceUnitDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _DeviceUnitRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        DeviceUnitDto DeviceUnitDto = _mapper.Map<DeviceUnitDto>(entity);


        return new ServiceResultSuccess(DeviceUnitDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _DeviceUnitRepository.DeleteAsync(id, current_user_id);
    }

}