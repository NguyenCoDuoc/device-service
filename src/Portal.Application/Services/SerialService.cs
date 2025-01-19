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
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public SerialService(ISerialRepository SerialRepository, IDeviceRepository deviceRepository, ILocationRepository locationRepository,IMapper mapper)
    {
        _SerialRepository = SerialRepository;
        _DeviceRepository = deviceRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<SerialDto>> GetPagingAsync(SerialDtoSearch pagingParams)
    {
        var result = new PagingResult<SerialDto>()
        {
            PageSize = pagingParams.ItemsPerPage,
            CurrentPage = pagingParams.Page
        };

        var where = new List<string>();
        var param = new Dictionary<string, object>();

        // Hàm giúp thêm điều kiện tìm kiếm vào where clause
        void AddSearchCondition(string field, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                where.Add($"(@{field} IS NULL OR {field} ILIKE @{field})");
                param.Add(field, $"%{value}%");
            }
        }

        void AddExactCondition(string field, object value)
        {
            if (value != null && value is long longValue && longValue != 0)
            {
                where.Add($"(@{field} IS NULL OR {field} = @{field})");
                param.Add(field, value);
            }
        }

        // Thêm các điều kiện tìm kiếm cho từng trường
        AddSearchCondition("description", pagingParams.Term);
        AddExactCondition("location_id", pagingParams.LocationId);

        // Kết hợp các điều kiện lại với nhau
        var whereClause = where.Count > 0 ? string.Join(" AND ", where) : "";

        // Lấy dữ liệu từ repository
        var data = await _SerialRepository.GetPageAsync<SerialDto>(
            pagingParams.Page, pagingParams.ItemsPerPage,
            order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc,
            param: param, where: whereClause);

        // Lấy tất cả các location từ service
        var locations = await _locationRepository.GetAllAsync();

        // Tạo một dictionary để tra cứu nhanh locationName theo locationId
        var locationDictionary = locations.ToDictionary(l => l.ID, l => l.Name);

        // Gán tên vị trí cho từng SerialDto
        foreach (var serial in data.Data)
        {
            if (serial.LocationId.HasValue && locationDictionary.ContainsKey(serial.LocationId.Value))
            {
                serial.LocatioName = locationDictionary[serial.LocationId.Value];
            }
        }

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
        long newId = 1; // Mặc định nếu chưa có bản ghi nào cho DeviceCode

        if (lastSerial != null)
        {
            // Lấy ID của serial cuối cùng và cộng 1
            newId = lastSerial.ID + 1;
        }
        else
        {
            // Nếu không tìm thấy lastSerial, lấy ID lớn nhất từ bảng Serial
            var maxId = await _SerialRepository.GetMaxSerialIdAsync();
            newId = maxId.HasValue ? maxId.Value + 1 : 1; // Nếu không có ID lớn nhất, sử dụng ID bắt đầu từ 1
        }

        // Tạo SerialCode theo mẫu: <DeviceCode><ID tự tăng>
        model.SerialCode = $"{model.DeviceCode}{newId}";

        model.SerialCode = "SER_" + model.SerialCode;

        var validator = new SerialValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        // Thêm bản ghi serial vào cơ sở dữ liệu
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

    public async Task<List<SerialAttributeDto>> GetSerialAttributes(long serialId)
    {
        return _mapper.Map<List<SerialAttributeDto>>(await _SerialRepository.GetSerialAttributes(serialId));
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