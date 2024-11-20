using Sun.Core.Share.Helpers.Params;
using System;

namespace DeviceService.Application.DTOS.Supplier
{
	public class SupplierSearchParam  : SearchParam
	{
		public int? Status { get; set; }
	}
}
