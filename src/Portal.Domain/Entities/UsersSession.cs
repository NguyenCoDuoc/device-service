using Sun.Core.DataAccess.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Domain.Entities
{
    [Table("users_session")]
    public class UsersSession : CrudFieldEntity<long>
    {
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("identity_refresh_token_id")]
        public string IdentityRefreshTokenId { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("issued_time")]
        public DateTime IssuedTime { get; set; }
        [Column("expire_time")]
        public DateTime ExpireTime { get; set; }
        [Column("remember_me")]
        public bool RememberMe { get; set; }
    }
}
