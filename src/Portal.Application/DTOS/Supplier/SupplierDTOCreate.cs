namespace DeviceService.Application.DTOS.Supplier
{
    public class SupplierDTOCreate : SupplierDTO
    {
        public SupplierDTOCreate() {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
