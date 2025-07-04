using System;

namespace Domain.Entity
{
    public class TestOrder : Base
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public TestOrderStatus Status { get; set; }
        public DeliveryKitStatus DeliveryKitStatus { get; set; }
        public DateTime? KitSendDate { get; set; }
        public int SampleMethodId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentLocation { get; set; }
        public int? AppointmentStaffId { get; set; }

        // Navigation properties
        public UserAccount User { get; set; }
        public Service Service { get; set; }
        public SampleMethod SampleMethod { get; set; }
        public UserAccount AppointmentStaff { get; set; }
        public ICollection<Sample> Samples { get; set; }
    }

    public enum TestOrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        Completed = 2,
        Cancelled = 3
    }

    public enum DeliveryKitStatus
    {
        NotSent = 0,
        Sent = 1,
    }
} 