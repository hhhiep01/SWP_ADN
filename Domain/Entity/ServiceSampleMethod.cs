using System.Collections.Generic;

namespace Domain.Entity
{
    public class ServiceSampleMethod : Base
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int SampleMethodId { get; set; }

        // Navigation properties
        public Service Service { get; set; }
        public SampleMethod SampleMethod { get; set; }
    }
} 