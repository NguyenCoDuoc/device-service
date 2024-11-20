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

public class LocationService
    : ILocationService
{
    private readonly ILocationRepository _LocationRepository;
    private readonly IMapper _mapper;
    private readonly long current_Location_id;

    public LocationService(ILocationRepository LocationRepository, IMapper mapper)
    {
        _LocationRepository = LocationRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<LocationDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<LocationDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");

        var data = await _LocationRepository.GetPageAsync<LocationDto>(pagingParams.Page, pagingParams.ItemsPerPage,
        order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<LocationDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(LocationDtoCreate model)
    {
        model.CreatedBy = current_Location_id;
        model.CreatedDate = DateTime.Now;

        var validator = new LocationValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _LocationRepository.InsertAsync(_mapper.Map<Location>(model));

        return new ServiceResultSuccess(_mapper.Map<LocationDto>(data));
    }

    public async Task<LocationDto> UpdateAsync(LocationDto model)
    {
        var entity = _mapper.Map<Location>(model);
        var data = await _LocationRepository.UpdateAsync(entity);

        return _mapper.Map<LocationDto>(data);
    }

    public async Task<IEnumerable<LocationDtoDetail>> GetAllAsync()
    {
        var data = await _LocationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LocationDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _LocationRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        LocationDto LocationDto = _mapper.Map<LocationDto>(entity);


        return new ServiceResultSuccess(LocationDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _LocationRepository.DeleteAsync(id, current_Location_id);
    }

}