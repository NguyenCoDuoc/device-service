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

public class DeviceService
    : IDeviceService
{
    private readonly IDeviceRepository _DeviceRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public DeviceService(IDeviceRepository DeviceRepository, IMapper mapper)
    {
        _DeviceRepository = DeviceRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<DeviceDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<DeviceDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("description", $"%{pagingParams.Term}%");
        var data = await _DeviceRepository.GetPageAsync<DeviceDto>(pagingParams.Page, pagingParams.ItemsPerPage,
               order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);

        result.Data = _mapper.Map<List<DeviceDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(DeviceDtoCreate model)
    {
        model.CreatedBy = current_user_id;
        model.CreatedDate = DateTime.Now;

        var validator = new DeviceValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _DeviceRepository.InsertAsync(_mapper.Map<Device>(model));

        // Tiếp tục xử lý và trả về kết quả...
        return new ServiceResultSuccess(data);
    }

    public async Task<DeviceDto> UpdateAsync(DeviceDto model)
    {
        var entity = _mapper.Map<Device>(model);
        var data = await _DeviceRepository.UpdateAsync(entity);

        return _mapper.Map<DeviceDto>(data);
    }

    public async Task<IEnumerable<BaseDto>> GetAllAsync()
    {
        var data = await _DeviceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BaseDto>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _DeviceRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        DeviceDto DeviceDto = _mapper.Map<DeviceDto>(entity);


        return new ServiceResultSuccess(DeviceDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _DeviceRepository.DeleteAsync(id, current_user_id);
    }

}