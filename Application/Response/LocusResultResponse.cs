using System;

namespace Application.Response
{
    public class LocusResultResponse
    {
        public int Id { get; set; }
        public int SampleId { get; set; }
        public string LocusName { get; set; }
        public string FirstAllele { get; set; }
        public string SecondAllele { get; set; }
    }
} 