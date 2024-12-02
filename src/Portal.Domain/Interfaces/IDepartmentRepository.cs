using DeviceService.Domain.Entities;
using Microsoft.Identity.Client;
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

    /// <summary>
    /// ds phòng ban tối giản
    /// </summary>
    /// <param name="tenant_id"></param>
    /// DUOCNC 20240212
    Task<List<Department?>> GetDepartmentSimple(long tenant_id);
}