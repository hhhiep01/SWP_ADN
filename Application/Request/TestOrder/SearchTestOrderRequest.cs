using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.TestOrder
{
    public class SearchTestOrderRequest
    {
        public TestOrderStatus? TestOrderStatus { get; set; }
        public DeliveryKitStatus? DeliveryKitStatus { get; set; }
        public int? ServiceId { get; set; }
        public int? SampleMethodId { get; set; }
        public int? AppointmentStaffId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
