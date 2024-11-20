﻿namespace DeviceService.Application.DTOS
{
    public class BaseDto
    {
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
    }
}