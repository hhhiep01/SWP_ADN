using System;

namespace Domain.Entity
{
    public class Result : Base
    {
        public int Id { get; set; }
        public int SampleId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Conclusion { get; set; }
        public string FilePath { get; set; }

        // Navigation properties
        public virtual Sample Sample { get; set; }
    }
} 