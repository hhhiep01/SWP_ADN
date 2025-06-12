using System.Collections.Generic;

namespace Domain.Entity
{
    public class Service : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ServiceType ServiceType { get; set; }
        //public CollectionType CollectionType { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public ICollection<ServiceSampleMethod> ServiceSampleMethods { get; set; }
        public List<TestOrder> TestOrders { get; set; }
    }

    
} 