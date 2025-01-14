using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Domain.Repositories
{
    public class DeviceRepository : DapperRepository<Device>, IDeviceRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public DeviceRepository() : base()
        {


        }
        public async Task<Device> GetByCodeAsync(string code)
        {
            {
                var query = @"
                    SELECT * FROM device 
                    WHERE Code = @Code";
                var parameters = new Dictionary<string, object>
                {
                    { "@Code", code }
                };

                return await QueryFirstOrDefaultAsync<Device?>(query, parameters);
            }
        }

        /// <summary>
        /// Get device  attributes by device  id
        /// </summary>
        /// <param name="deviceId">Device  id</param>
        /// <returns>List of device  attributes</returns>
        public async Task<List<DeviceAttribute>> GetDeviceAttributes(long deviceId)
        {
            var sql = @"SELECT 
                        dat.id as Id,
                        dat.description as Description,
                        dat.device_id as DeviceId,
                        dat.attribute_id as AttributeId,
                        dat.attribute_value_id as AttributeValueId,
                        a.name as AttributeName,
                        av.value as AttributeValue
                        FROM device_attribute dat
                        LEFT JOIN attribute a ON dat.attribute_id = a.id
                        LEFT JOIN attribute_value av ON dat.attribute_value_id = av.id
                        WHERE dat.is_deleted=false";

            return (await QueryAsync<DeviceAttribute>(sql, new Dictionary<string, object> { { "deviceId", deviceId } })).ToList();
        }


        /// <summary>
        /// Add device  attribute
        /// </summary>
        /// <param name="deviceAttribute">Device  attribute</param>
        /// <returns>Device  attribute id</returns>
        /// DUOCNC 20241106
        public async Task<int> AddDeviceAttribute(DeviceAttribute deviceAttribute, long current_user_id)
        {
            var sql = @"INSERT INTO device_attribute 
                        (description, device_id, attribute_id, attribute_value_id, created_by, is_deleted) 
                        VALUES  
                        (@Description, @DeviceId, @AttributeId, @AttributeValueId, @CreatedBy, false)
                        RETURNING id";

            var parameters = new Dictionary<string, object>
            {
                { "Description", deviceAttribute.Description },
                { "DeviceId", deviceAttribute.DeviceId },
                { "AttributeId", deviceAttribute.AttributeId },
                { "AttributeValueId", deviceAttribute.AttributeValueId },
                { "CreatedBy", current_user_id }
            };

            return await ExecuteScalarAsync<int>(sql, parameters);
        }

        /// <summary>
        /// deleted logic
        /// </summary>
        /// <param name="id">Device  attribute id</param>
        /// <returns>Device  attribute id</returns>
        public async Task<int> DeleteDeviceAttribute(long id, long current_user_id)
        {
            var sql = @"UPDATE device_attribute SET is_deleted = true, deleted_by = @DeletedBy, deleted_date = @DeletedDate WHERE id = @id";
            var parameters = new Dictionary<string, object>
            {
                { "id", id },
                { "DeletedBy", current_user_id },
                { "DeletedDate", DateTime.Now }
            };

            return await ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<Device> GetLastDeviceByTypeNameAsync(string deviceTypeCode)
        {
            var sql = @"SELECT * FROM device 
                WHERE code LIKE @DeviceTypeCode || '%' 
                AND is_deleted = false
                ORDER BY code DESC 
                LIMIT 1";

            var parameters = new Dictionary<string, object>
            {
                { "DeviceTypeCode", deviceTypeCode }
            };

            return await QueryFirstOrDefaultAsync<Device>(sql, parameters);
        }

    }
}
