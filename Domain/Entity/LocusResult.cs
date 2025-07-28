using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class LocusResult : Base
    {
        public int Id { get; set; }
        public int SampleId { get; set; }
        public string LocusName { get; set; }
        public string FirstAllele { get; set; }
        public string SecondAllele { get; set; }

        public Sample Sample { get; set; }
    }
}
