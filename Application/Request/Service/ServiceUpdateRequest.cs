using Domain.Entity;
using System.Collections.Generic;

namespace Application.Request.Service
{
    public class ServiceUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       /* public ServiceType ServiceType { get; set; }
        public CollectionType CollectionType { get; set; }*/
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<int> SampleMethodIds { get; set; }
        public string Image { get; set; }
    }
} 