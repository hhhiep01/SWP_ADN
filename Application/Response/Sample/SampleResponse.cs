using Application.Response.SampleMethod;
using Application.Response.TestOrder;
using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Application.Response.Sample
{
    public class SampleResponse
    {
        public int Id { get; set; }
        public DateTime? CollectionDate { get; set; }
        public SampleStatus SampleStatus { get; set; }
        public string Notes { get; set; }
        public int? CollectedBy { get; set; }
        public string CollectorName { get; set; }
        public TestOrderShortResponse TestOrder { get; set; }
        public ResultResponse Result { get; set; }
        //public SampleMethodResponse SampleMethod { get; set; }
        public string? ShippingProvider { get; set; }
        public string? TrackingNumber { get; set; }
        public string ParticipantName { get; set; }
        public string Relationship { get; set; }
        public string SampleCode { get; set; }
        public List<Application.Response.LocusResultResponse> LocusResults { get; set; }

    }
} 