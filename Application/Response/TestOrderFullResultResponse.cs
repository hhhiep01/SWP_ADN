using Application.Response.Sample;
using Application.Response.TestOrder;
using System.Collections.Generic;

namespace Application.Response
{
    public class TestOrderFullResultResponse
    {
        public int TestOrderId { get; set; }
        public TestOrderWithResultResponse OrderInfo { get; set; }
    }

    public class TestOrderWithResultResponse : TestOrderResponse
    {
        public new List<SampleWithResultResponse> Samples { get; set; }
    }

    public class SampleWithResultResponse : SampleResponse
    {
        public ResultResponse Result { get; set; }
        public List<LocusResultResponse> LocusResults { get; set; }
    }
} 