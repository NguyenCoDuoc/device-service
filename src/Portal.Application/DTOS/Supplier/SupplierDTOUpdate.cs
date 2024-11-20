namespace DeviceService.Application.DTOS.Supplier
{
    public class SupplierDTOUpdate : SupplierDTO
    {
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public SupplierDTOUpdate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
