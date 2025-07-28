using System;
using System.Collections.Generic;

namespace Application.Request
{
    public class LocusResultRequest
    {
        public int SampleId { get; set; }
        public List<LocusAlleleDto> LocusAlleles { get; set; }
    }

    public class LocusAlleleDto
    {
        public string LocusName { get; set; }
        public string FirstAllele { get; set; }
        public string SecondAllele { get; set; }
    }
} 