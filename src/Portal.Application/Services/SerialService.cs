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
using Sun.Core.Share.Constants;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DeviceService.Application.Services;

public class SerialService
    : ISerialService
{
    private readonly ISerialRepository _SerialRepository;
    private readonly IDeviceRepository _DeviceRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public SerialService(ISerialRepository SerialRepository, IDeviceRepository deviceRepository, IMapper mapper)
    {
        _SerialRepository = SerialRepository;
        _DeviceRepository = deviceRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<SerialDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<SerialDto>()
        { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

        var where = "";
        var param = new Dictionary<string, object>();

        if (!string.IsNullOrWhiteSpace(pagingParams.Term))
        {
            where = "((serial_number IS NULL OR serial_number ILIKE @serial_number)" +
                " OR (serial_code IS NULL OR serial_code ILIKE @serial_code)" +
                " OR description ILIKE @description)";

            param.Add("serial_number", $"%{pagingParams.Term}%");
            param.Add("serial_code", $"%{pagingParams.Term}%");
            param.Add("description", $"%{pagingParams.Term}%");
        }

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
        model.IsDeleted = 0;

        // Tạo mã tự động cho serial_number
        var lastSerial = await _SerialRepository.GetLastSerialByDeviceCodeAsync(model.DeviceCode);
        if (lastSerial != null)
        {
            // Tìm số cuối cùng trong mã device hiện tại
            string currentCode = lastSerial.SerialCode;
            string numberPart = string.Empty;

            for (int i = currentCode.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(currentCode[i]))
                {
                    numberPart = currentCode[i] + numberPart;
                }
                else
                {
                    break;
                }
            }

            if (int.TryParse(numberPart, out int lastNumber))
            {
                model.SerialCode = $"{model.DeviceCode}{(lastNumber + 1).ToString().PadLeft(numberPart.Length, '0')}";
            }
            else
            {
                model.SerialCode = $"{model.DeviceCode}1";
            }
        }
        else
        {
            // Nếu chưa có device nào của loại này
            model.SerialCode = $"{model.DeviceCode}1";
        }

        model.SerialCode = "SER_" + model.SerialCode;

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

    public async Task<ServiceResult> CopyAsync(long id, int times)
    {
        // Get original serial
        var query = @"SELECT  warranty_period WarrantyPeriod,
                           purchase_date PurchaseDate,
                           id Id,
                            device_id DeviceId,
                           serial_code SerialCod,
                           serial_number SerialNumber,
                           location_id LocationId
                    FROM serial WHERE id = @id and is_deleted = false;";
        var parameters = new Dictionary<string, object>
        {
            { "@id", id }
        };

        var origin = await _SerialRepository.QueryFirstOrDefaultAsync<Serial?>(query, parameters);
        if (origin == null)
        {
            return new ServiceResultError("Serial không tồn tại");
        }

        var query1 = @"SELECT * FROM device WHERE id = @id and is_deleted = false";
        var parameters1 = new Dictionary<string, object>
        {
            { "@id", origin.DeviceId }
        };
        var device = await _DeviceRepository.QueryFirstOrDefaultAsync<Device?>(query1, parameters1);
        if (device == null)
        {
            return new ServiceResultError("Device không tồn tại");
        }

        var copiedSerials = new List<SerialDto>();

        // Copy serial records
        for (int i = 0; i < times; i++)
        {
            var serialCopy = new SerialDtoCreate
            {
                DeviceCode = device.Code,
                Description = origin.Description,
                CreatedBy = current_user_id,
                CreatedDate = DateTime.Now,
                IsDeleted = 0,
                PurchaseDate = origin.PurchaseDate,
                DeviceId = origin.DeviceId,
                WarrantyPeriod = origin.WarrantyPeriod,
                LocationId = origin.LocationId,

            };

            // Create new serial with auto-generated code
            var copyResult = await CreateAsync(serialCopy);
            if (copyResult.Code == CommonConst.Success)
            {
                copiedSerials.Add((SerialDto)copyResult.Data);
            }
            else
            {
                return copyResult; // Return error if creation fails
            }
        }

        // Get original serial attributes
        var originalAttributes = await GetSerialAttributes(id);

        // Copy attributes for each new serial
        foreach (var copiedSerial in copiedSerials)
        {
            foreach (var attribute in originalAttributes)
            {
                var attributeCopy = new SerialAttributeDto
                {
                    SerialId = (long)copiedSerial.Id,
                    AttributeId = attribute.AttributeId,
                    Description = "copy",
                    CreatedBy = current_user_id,
                    AttributeValueId = attribute.AttributeValueId // Fix for CS9035
                };

                await AddSerialAttribute(attributeCopy);
            }
        }

        return new ServiceResultSuccess(new
        {
            Message = $"Đã sao chép thành công {times} bản ghi",
            CopiedSerials = copiedSerials
        });
    }
}