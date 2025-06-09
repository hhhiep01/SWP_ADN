using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SampleMethod : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<ServiceSampleMethod> ServiceSampleMethods { get; set; }
    }
} 