using DeviceService.Application.Interfaces;
using AutoMapper;
using Sun.Core.Share.Helpers.Params;
using Sun.Core.Share.Helpers.Results;
using Sun.Core.Share.Helpers.Util;
using DeviceService.Domain.Interfaces;
using DeviceService.Application.DTOS.Users;
using DeviceService.Domain.Entities;
using DeviceService.Application.Validators;

namespace DeviceService.Application.Services;

public class UserService
    : IUserService
{
    private readonly IUsersRepository _UserRepository;
    private readonly IMapper _mapper;
    private readonly long current_User_id;

    public UserService(IUsersRepository UserRepository, IMapper mapper)
    {
        _UserRepository = UserRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<UsersDTO>> GetPagingAsync(SearchParam pagingParams)
    {
        var result = new PagingResult<UsersDTO>()
            { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };
        var where = "name ILIKE @name OR description ILIKE @description";
        var param = new Dictionary<string, object>();
        param.Add("name", $"%{pagingParams.Term}%");
        param.Add("description", $"%{pagingParams.Term}%");
        var data = await _UserRepository.GetPageAsync<UsersDTO>(pagingParams.Page, pagingParams.ItemsPerPage,
                order: pagingParams.SortBy, sortDesc: pagingParams.SortDesc, param: param, where: where);
        result.Data = _mapper.Map<List<UsersDTO>>(data.Data);
        result.TotalRows = data.TotalRow;
        return result;
    }

    public async Task<ServiceResult> CreateAsync(UsersDTOCreate model)
    {
        model.CreatedBy = current_User_id;
        model.CreatedDate = DateTime.Now;

        var validator = new UsersCreateValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResultError(errorMessage);
        }

        var data = await _UserRepository.InsertAsync(_mapper.Map<Users>(model));

        return new ServiceResultSuccess(_mapper.Map<UsersDTO>(data));
    }

    public async Task<UsersDTO> UpdateAsync(UsersDTO model)
    {
        var entity = _mapper.Map<Users>(model);
        var data = await _UserRepository.UpdateAsync(entity);

        return _mapper.Map<UsersDTO>(data);
    }

    public async Task<IEnumerable<UsersDTO>> GetAllAsync()
    {
        var data = await _UserRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UsersDTO>>(data);
    }

    public async Task<ServiceResult> GetByIdAsync(long id)
    {

        var entity = await _UserRepository.GetAsync(id);
        if (!entity.IsNotEmpty())
            return new ServiceResultError("Phòng ban không tồn tại");

        UsersDTO UsersDTO = _mapper.Map<UsersDTO>(entity);


        return new ServiceResultSuccess(UsersDTO);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _UserRepository.DeleteAsync(id, current_User_id);
    }
}