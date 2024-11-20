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

public class JobtitleService
    : IJobtitleService
{
    private readonly IJobtitleRepository _JobtitleRepository;
    private readonly IMapper _mapper;
    private readonly long current_Jobtitle_id;

    public JobtitleService(IJobtitleRepository JobtitleRepository, IMapper mapper)
    {
        _JobtitleRepository = JobtitleRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<JobtitleDto>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<JobtitleDto>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");

        var data = await _JobtitleRepository.GetPageAsync<JobtitleDto>(pagingParams.Page, pagingParams.ItemsPerPage,
            order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<JobtitleDto>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(JobtitleDtoCreate model)
    {
        model.CreatedBy = current_Jobtitle_id;
        model.CreatedDate = DateTime.Now;

        var validator = new JobtitleValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _JobtitleRepository.InsertAsync(_mapper.Map<Jobtitle>(model));

        return new ServiceResultSuccess(_mapper.Map<JobtitleDto>(data));
    }

    public async Task<JobtitleDto> UpdateAsync(JobtitleDto model)
    {
        var entity = _mapper.Map<Jobtitle>(model);
        var data = await _JobtitleRepository.UpdateAsync(entity);

        return _mapper.Map<JobtitleDto>(data);
    }

    public async Task<IEnumerable<JobtitleDtoDetail>> GetAllAsync()
    {
        var data = await _JobtitleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<JobtitleDtoDetail>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _JobtitleRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        JobtitleDto JobtitleDto = _mapper.Map<JobtitleDto>(entity);


        return new ServiceResultSuccess(JobtitleDto);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _JobtitleRepository.DeleteAsync(id, current_Jobtitle_id);
    }

}