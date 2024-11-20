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

public class SerialLocationService
    : ISerialLocationService
{
    private readonly ISerialLocationRepository _SerialLocationRepository;
    private readonly IMapper _mapper;

    public SerialLocationService(ISerialLocationRepository SerialLocationRepository, IMapper mapper)
    {
        _SerialLocationRepository = SerialLocationRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<SerialLocationDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<SerialLocationDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("description", $"%{pagingParams.Term}%");

        var data = await _SerialLocationRepository.GetPageAsync<LocationDto>(pagingParams.Page, pagingParams.ItemsPerPage,
            order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<SerialLocationDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(SerialLocationDtoCreate model)
    {
        model.CreatedDate = DateTime.Now;

        var validator = new SerialLocationValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _SerialLocationRepository.InsertAsync(_mapper.Map<SerialLocation>(model));

        return new ServiceResultSuccess(_mapper.Map<SerialLocationDto>(data));
    }

    public async Task<SerialLocationDto> UpdateAsync(SerialLocationDto model)
    {
        var entity = _mapper.Map<SerialLocation>(model);
        var data = await _SerialLocationRepository.UpdateAsync(entity);

        return _mapper.Map<SerialLocationDto>(data);
    }

    public async Task<IEnumerable<SerialLocationDtoDetail>> GetAllAsync()
    {
        var data = await _SerialLocationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SerialLocationDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _SerialLocationRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        SerialLocationDto SerialLocationDto = _mapper.Map<SerialLocationDto>(entity);


        return new ServiceResultSuccess(SerialLocationDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _SerialLocationRepository.DeleteAsync(id);
    }

}