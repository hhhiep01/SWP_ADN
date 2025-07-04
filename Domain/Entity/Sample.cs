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

        // Navigation properties
        public  TestOrder TestOrder { get; set; }
        public  SampleMethod SampleMethod { get; set; }
        public  UserAccount Collector { get; set; } 
        public Result Result { get; set; }
    }
    public enum SampleStatus
        {
            Collected = 1,    // Đã thu thập
            Received = 2,     // Đã nhận
            Testing = 3,      // Đang xét nghiệm
            Completed = 4     // Hoàn thành
        }
} 