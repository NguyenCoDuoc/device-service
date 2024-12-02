using System.Data;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Domain.Repositories
{
    public class DepartmentRepository : DapperRepository<Department>, IDepartmentRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public DepartmentRepository() : base()
        {

        }

        public async Task<List<Department?>> GetDepartmentsByUserId(long userId)
        {
            var query = @"
                        SELECT d.*
                        FROM department_user ud
                        JOIN department d ON ud.department_id = d.id
                        WHERE ud.user_id = @userId
                    ";
            var parameters = new Dictionary<string, object>
            {
                { "@userId", userId }
            };

            var departments = await QueryAsync<Department?>(query, parameters);

            return departments.ToList();
        }

        public async Task<List<Department?>> GetDepartmentSimple(long tenant_id)
        {
            var query = @"
                        SELECT d.code,d.name
                        JOIN department d 
                        WHERE d.tenant_id = @tenant_id
                    ";
            var parameters = new Dictionary<string, object>
            {
                { "@tenant_id", tenant_id }
            };

            var departments = await QueryAsync<Department?>(query, parameters);

            return departments.ToList();
        }

    }
}
