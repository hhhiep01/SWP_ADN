using Domain.Entity;
using System;

namespace Application.Request.TestOrder
{
    public class CreateTestOrderRequest
    {
        public int ServiceId { get; set; }
        public int SampleMethodId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentLocation { get; set; }
        //public int? AppointmentStaffId { get; set; }
    }

    public class UpdateTestOrderRequest
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int SampleMethodId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentLocation { get; set; }
        public int? AppointmentStaffId { get; set; }
    }

    public class UpdateTestOrderStatusRequest
    {
        public int Id { get; set; }
        public TestOrderStatus TestOrderStatus { get; set; }
    }

    public class UpdateDeliveryKitStatusRequest
    {
        public int Id { get; set; }
        public DeliveryKitStatus DeliveryKitStatus { get; set; }
    }

    public class UpdateAppointmentStatusRequest
    {
        public int Id { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}