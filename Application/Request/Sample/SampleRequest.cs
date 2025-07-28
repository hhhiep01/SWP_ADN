using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Request.Sample
{
    public class CreateSamplesRequest
    {
        public int TestOrderId { get; set; }

        public string ShippingProvider { get; set; }

        public string TrackingNumber { get; set; }

        public List<SampleRequest> Participants { get; set; }
    }

    public class SampleRequest
    {
        public DateTime? CollectionDate { get; set; }
        public SampleStatus SampleStatus { get; set; }
        public string? Notes { get; set; }

        
        public string ParticipantName { get; set; }

        public string Relationship { get; set; }
    }
} 