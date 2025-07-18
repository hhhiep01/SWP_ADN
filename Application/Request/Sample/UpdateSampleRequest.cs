using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Request.Sample
{
    public class UpdateSampleRequest
    {
        [Required]
        public int Id { get; set; }
        public int TestOrderId { get; set; }
        public DateTime? CollectionDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public SampleStatus SampleStatus { get; set; }
        public string Notes { get; set; }
        //public int? CollectedBy { get; set; }
    }
} 