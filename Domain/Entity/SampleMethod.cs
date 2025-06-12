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
        public bool IsActive { get; set; }

        // Navigation properties
        public List<ServiceSampleMethod> ServiceSampleMethods { get; set; }
        public List<TestOrder> TestOrders { get; set; }
    }
} 