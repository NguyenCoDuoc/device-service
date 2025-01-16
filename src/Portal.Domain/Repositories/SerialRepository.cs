using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sun.Core.DataAccess.Repositories;
using Sun.Core.Share.Helpers.Util;
using Serial = DeviceService.Domain.Entities.Serial;

namespace DeviceService.Domain.Repositories
{
    public class SerialRepository : DapperRepository<Serial>, ISerialRepository
    {
        public override string ConnectionString()
        {
            return AppSettingsManager.GetConnectionString("tdsoftwareConnectionString");
        }
        public async Task<List<SerialAttribute>> GetSerialAttributes(long serialId)
        {
            var sql = @"SELECT 
                        dat.id as Id,
                        dat.description as Description,
                        dat.serial_id as SerialId,
                        dat.attribute_id as AttributeId,
                        dat.attribute_value_id as AttributeValueId,
                        a.name as AttributeName,
                        av.value as AttributeValue
                        FROM serial_attribute dat
                        LEFT JOIN attribute a ON dat.attribute_id = a.id
                        LEFT JOIN attribute_value av ON dat.attribute_value_id = av.id
                        WHERE dat.is_deleted=false and serial_id=@serialId";

            return (await QueryAsync<SerialAttribute>(sql, new Dictionary<string, object> { { "serialId", serialId } })).ToList();
        }


        /// <summary>
        /// Add serial attribute
        /// </summary>
        /// <param name="serialAttribute">Serial  attribute</param>
        /// <returns>Serial  attribute id</returns>
        /// DUOCNC 20241106
        public async Task<int> AddSerialAttribute(SerialAttribute serialAttribute, long current_user_id)
        {
            var sql = @"INSERT INTO serial_attribute 
                        (description, serial_id, attribute_id, attribute_value_id, created_by, is_deleted) 
                        VALUES  
                        (@Description, @SerialId, @AttributeId, @AttributeValueId, @CreatedBy, false)
                        RETURNING id";

            var parameters = new Dictionary<string, object>
            {
                { "Description", serialAttribute.Description },
                { "SerialId", serialAttribute.SerialId },
                { "AttributeId", serialAttribute.AttributeId },
                { "AttributeValueId", serialAttribute.AttributeValueId },
                { "CreatedBy", current_user_id }
            };

            return await ExecuteScalarAsync<int>(sql, parameters);
        }

        /// <summary>
        /// deleted logic
        /// </summary>
        /// <param name="id">Serial  attribute id</param>
        /// <returns>Serial  attribute id</returns>
        public async Task<int> DeleteSerialAttribute(long id, long current_user_id)
        {
            var sql = @"UPDATE serial_attribute SET is_deleted = true, deleted_by = @DeletedBy, deleted_date = @DeletedDate WHERE id = @id";
            var parameters = new Dictionary<string, object>
            {
                { "id", id },
                { "DeletedBy", current_user_id },
                { "DeletedDate", DateTime.Now }
            };

            return await ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<Serial> GetLastSerialByDeviceCodeAsync(string deviceCode)
        {
            var serialCode = "SER_" + deviceCode;
            var sql = @"SELECT id ID, serial_number SerialNumber, serial_code SerialCode FROM serial
                WHERE serial_code LIKE @serialCode || '%'
                ORDER BY serial_code DESC
                LIMIT 1";

            var parameters = new Dictionary<string, object>
            {
                { "@serialCode", serialCode }
            };

            return await QueryFirstOrDefaultAsync<Serial>(sql, parameters);
        }

        public async Task<int?> GetMaxSerialIdAsync()
        {
            var sql = @"SELECT MAX(Id) AS MaxId FROM serial";

            var result = await QueryFirstOrDefaultAsync<int?>(sql, new Dictionary<string, object>());

            return result;
        }

    }
}
