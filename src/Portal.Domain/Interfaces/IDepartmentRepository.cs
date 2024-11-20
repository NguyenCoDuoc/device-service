using DeviceService.Domain.Entities;
using Sun.Core.DataAccess.Interfaces;

namespace DeviceService.Domain.Interfaces;

public interface IDepartmentRepository : IDapperRepository<Department>
{
    /// <summary>
    /// Danh sách phòng ban theo user id
    /// </summary>
    /// <param name="userId"></param>
    /// DUOCNC 12082024
    Task<List<Department?>> GetDepartmentsByUserId(long userId);
}