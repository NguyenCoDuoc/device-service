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

public class SerialService
    : ISerialService
{
    private readonly ISerialRepository _SerialRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public SerialService(ISerialRepository SerialRepository, IMapper mapper)
    {
        _SerialRepository = SerialRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<SerialDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<SerialDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "serial_number ILIKE @serial_number OR description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("serial_number", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");

        var data = await _SerialRepository.GetPageAsync<SerialDto>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);

        result.Data = _mapper.Map<List<SerialDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(SerialDtoCreate model)
    {
        model.CreatedBy = current_user_id;
        model.CreatedDate = DateTime.Now;

        var validator = new SerialValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _SerialRepository.InsertAsync(_mapper.Map<Serial>(model));

        return new ServiceResultSuccess(_mapper.Map<SerialDto>(data));
    }

    public async Task<SerialDto> UpdateAsync(SerialDto model)
    {
        var entity = _mapper.Map<Serial>(model);
        var data = await _SerialRepository.UpdateAsync(entity);

        return _mapper.Map<SerialDto>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _SerialRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        SerialDto SerialDto = _mapper.Map<SerialDto>(entity);


        return new ServiceResultSuccess(SerialDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _SerialRepository.DeleteAsync(id, current_user_id);
    }

    public async Task<List<SerialAttributeDto>> GetSerialAttributes(long deviceId)
    {
        return _mapper.Map<List<SerialAttributeDto>>(await _SerialRepository.GetSerialAttributes(deviceId));
    }

    public async Task<int> AddSerialAttribute(SerialAttributeDto serialAttribute)
    {
        return await _SerialRepository.AddSerialAttribute(_mapper.Map<SerialAttribute>(serialAttribute), current_user_id);
    }

    public async Task<int> DeleteSerialAttribute(long id)
    {
        return await _SerialRepository.DeleteSerialAttribute(id, current_user_id);
    }
}