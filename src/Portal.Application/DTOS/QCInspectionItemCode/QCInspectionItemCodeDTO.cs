
namespace Portal.Application.DTOS.QCInspectionItemCode
{
    public class InspectionDashboardDto
    {
        public long wait { get; set; }
        public long done { get; set; }
        public long processing { get; set; }
        public long completed { get; set; }
        public long shreject { get; set; }
        public long confirm { get; set; }

    }
}
