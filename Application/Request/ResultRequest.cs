namespace Application.Request
{
    public class ResultRequest
    {
        public int SampleId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Conclusion { get; set; }
        public string FilePath { get; set; }
    }
} 