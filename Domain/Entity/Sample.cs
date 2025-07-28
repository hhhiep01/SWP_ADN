using System;

namespace Domain.Entity
{
    public class Sample : Base
    {
        

        public int Id { get; set; }
        public int TestOrderId { get; set; }
        public int SampleMethodId { get; set; }
        public DateTime? CollectionDate { get; set; }
        public SampleStatus SampleStatus { get; set; }
        public string Notes { get; set; }
        public int? CollectedBy { get; set; }  
        public string? ShippingProvider { get; set; }
        public string? TrackingNumber { get; set; }
        public string? ParticipantName { get; set; }
        public string? Relationship { get; set; }
        public string? SampleCode { get; set; }

        // Navigation properties
        public  TestOrder TestOrder { get; set; }
        public  SampleMethod SampleMethod { get; set; }
        public  UserAccount Collector { get; set; } 
        public ICollection<LocusResult> LocusResults { get; set; }
    }
    public enum SampleStatus
        {
            Collected = 1,    // Đã thu thập
            Received = 2,     // Đã nhận
            Testing = 3,      // Đang xét nghiệm
            Completed = 4     // Hoàn thành
        }
} 