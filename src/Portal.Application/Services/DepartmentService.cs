using DeviceService.Application.DTOS;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Validators;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using AutoMapper;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using DeviceService.Application.DTOS.Country;
using DeviceService.Domain.Repositories;

namespace DeviceService.Application.Services;

public class DepartmentService
    : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly long current_user_id;

    public DepartmentService(IDepartmentRepository DepartmentRepository, IMapper mapper)
    {
        _departmentRepository = DepartmentRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<DepartmentDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<DepartmentDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR code ILIKE @code";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("code", $"%{pagingParams.Term}%");

        var data = await _departmentRepository.GetPageAsync<DepartmentDto>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);

        result.Data = _mapper.Map<List<DepartmentDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(DepartmentDtoCreate model)
    {
        model.CreatedBy = current_user_id;
        model.CreatedDate = DateTime.Now;

        var validator = new DepartmentValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _departmentRepository.InsertAsync(_mapper.Map<Department>(model));

        return new ServiceResultSuccess(_mapper.Map<DepartmentDto>(data));
    }

    public async Task<DepartmentDto> UpdateAsync(DepartmentDto model)
    {
        var entity = _mapper.Map<Department>(model);
        var data = await _departmentRepository.UpdateAsync(entity);

        return _mapper.Map<DepartmentDto>(data);
    }

    public async Task<IEnumerable<DepartmentDtoDetail>> GetAllAsync()
    {
        var data = await _departmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DepartmentDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _departmentRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        DepartmentDto DepartmentDto = _mapper.Map<DepartmentDto>(entity);


        return new ServiceResultSuccess(DepartmentDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _departmentRepository.DeleteAsync(id, current_user_id);
    }

    public async Task<List<DepartmentDtoDetail>> GetDepartmentsByUserId(long userId)
    {
        var data = await _departmentRepository.GetDepartmentsByUserId(userId);
        return _mapper.Map<List<DepartmentDtoDetail>>(data);
    }

}