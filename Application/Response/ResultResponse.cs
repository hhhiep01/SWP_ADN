namespace Application.Response
{
    public class ResultResponse
    {
        public int Id { get; set; }
        public int SampleId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Conclusion { get; set; }
        public string FilePath { get; set; }
        public string ServiceName { get; set; }
        public string SampleMethodName { get; set; }
    }
} 