using Application.Response.SampleMethod;
using Application.Response.TestOrder;
using Domain.Entity;
using System;

namespace Application.Response.Sample
{
    public class SampleResponse
    {
        public int Id { get; set; }
        public DateTime? CollectionDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public SampleStatus SampleStatus { get; set; }
        public string Notes { get; set; }
        public int? CollectedBy { get; set; }
        public string CollectorName { get; set; }
        public TestOrderShortResponse TestOrder { get; set; }
        public ResultResponse Result { get; set; }
        //public SampleMethodResponse SampleMethod { get; set; }

    }
} 