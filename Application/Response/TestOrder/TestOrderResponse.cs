using Application.Response.SampleMethod;
using Application.Response.Service;
using Domain.Entity;
using System;

namespace Application.Response.TestOrder
{
    public class TestOrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        /*public int ServiceId { get; set; }
        public string ServiceName { get; set; }*/
        public ServiceResponse Services { get; set; }
        public SampleMethodResponse SampleMethods { get; set; }
        public TestOrderStatus Status { get; set; }
        public DeliveryKitStatus DeliveryKitStatus { get; set; }
        public DateTime? KitSendDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string AppointmentLocation { get; set; }
        public int? AppointmentStaffId { get; set; }
        public string AppointmentStaffName { get; set; }
    }
}