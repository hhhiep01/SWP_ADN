using Application.Response.SampleMethod;
using Domain.Entity;
using System.Collections.Generic;

namespace Application.Response.Service
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       /* public ServiceType ServiceType { get; set; }
        public CollectionType CollectionType { get; set; }*/
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<SampleMethodResponse> SampleMethods { get; set; }
    }
} 