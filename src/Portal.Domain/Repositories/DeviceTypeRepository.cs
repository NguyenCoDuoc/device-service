using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;

namespace DeviceService.Domain.Repositories
{
    public class DeviceTypeRepository : DapperRepository<DeviceType>, IDeviceTypeRepository
    {

        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public DeviceTypeRepository() : base()
        {

        }

        /// <summary>
        /// Get device type attributes by device type id
        /// </summary>
        /// <param name="deviceTypeId">Device type id</param>
        /// <returns>List of device type attributes</returns>
        public async Task<List<DeviceTypeAttribute>> GetDeviceTypeAttributes(long deviceTypeId)
        {
            var sql = @"SELECT 
                        dat.id as Id,
                        dat.description as Description,
                        dat.device_type_id as DeviceTypeId,
                        dat.attribute_id as AttributeId,
                        dat.attribute_value_id as AttributeValueId,
                        a.name as AttributeName,
                        av.value as AttributeValue
                        FROM device_type_attribute dat 
                        LEFT JOIN attribute a ON dat.attribute_id = a.id
                        LEFT JOIN attribute_value av ON dat.attribute_value_id = av.id
                        WHERE device_type_id = @deviceTypeId and dat.is_deleted=false";

            return (await QueryAsync<DeviceTypeAttribute>(sql, new Dictionary<string, object> { { "deviceTypeId", deviceTypeId } })).ToList();
        }


        /// <summary>
        /// Add device type attribute
        /// </summary>
        /// <param name="deviceTypeAttribute">Device type attribute</param>
        /// <returns>Device type attribute id</returns>
        /// DUOCNC 20241106
        public async Task<int> AddDeviceTypeAttribute(DeviceTypeAttribute deviceTypeAttribute, long current_user_id)
        {
            var sql = @"INSERT INTO device_type_attribute 
                        (description, device_type_id, attribute_id, attribute_value_id, created_by, is_deleted) 
                        VALUES  
                        (@Description, @DeviceTypeId, @AttributeId, @AttributeValueId, @CreatedBy, false)
                        RETURNING id";

            var parameters = new Dictionary<string, object>
            {
                { "Description", deviceTypeAttribute.Description },
                { "DeviceTypeId", deviceTypeAttribute.DeviceTypeId },
                { "AttributeId", deviceTypeAttribute.AttributeId },
                { "AttributeValueId", deviceTypeAttribute.AttributeValueId },
                { "CreatedBy", current_user_id }
            };
            
            return await ExecuteScalarAsync<int>(sql, parameters);
        }       

        /// <summary>
        /// deleted logic
        /// </summary>
        /// <param name="id">Device type attribute id</param>
        /// <returns>Device type attribute id</returns>
        public async Task<int> DeleteDeviceTypeAttribute(long id, long current_user_id)
        {
            var sql = @"UPDATE device_type_attribute SET is_deleted = true, deleted_by = @DeletedBy, deleted_date = @DeletedDate WHERE id = @id";
            var parameters = new Dictionary<string, object>
            {
                { "id", id },
                { "DeletedBy", current_user_id },
                { "DeletedDate", DateTime.Now }
            };

            return await ExecuteScalarAsync<int>(sql, parameters);
        }
    }
}
