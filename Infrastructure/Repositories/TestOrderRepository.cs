using Application.Repository;
using Application.Request.TestOrder;
using Domain.Entity;
using LinqKit;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TestOrderRepository : GenericRepository<TestOrder>, ITestOrderRepository
    {
        public TestOrderRepository(AppDbContext context) : base(context)
        {
        }
        private ExpressionStarter<TestOrder> BuildPredicate(SearchTestOrderRequest req)
        {
            var pred = PredicateBuilder.New<TestOrder>(true);
            if (req.TestOrderStatus.HasValue)
                pred = pred.And(x => x.Status == req.TestOrderStatus.Value);
            if (req.DeliveryKitStatus.HasValue)
                pred = pred.And(x => x.DeliveryKitStatus == req.DeliveryKitStatus.Value);
            if (req.AppointmentStatus.HasValue)
                pred = pred.And(x => x.AppointmentStatus == req.AppointmentStatus.Value);
            if (req.ServiceId.HasValue)
                pred = pred.And(x => x.ServiceId == req.ServiceId.Value);
            if (req.SampleMethodId.HasValue)
                pred = pred.And(x => x.SampleMethodId == req.SampleMethodId.Value);
            if (req.AppointmentStaffId.HasValue)
                pred = pred.And(x => x.AppointmentStaffId == req.AppointmentStaffId.Value);
            if (!string.IsNullOrEmpty(req.PhoneNumber))
                pred = pred.And(x => x.PhoneNumber.Contains(req.PhoneNumber));
            if (!string.IsNullOrEmpty(req.Email))
                pred = pred.And(x => x.Email.Contains(req.Email));
            if (!string.IsNullOrEmpty(req.FullName))
                pred = pred.And(x => x.FullName.Contains(req.FullName));
            if (req.FromDate.HasValue)
                pred = pred.And(x => x.AppointmentDate >= req.FromDate.Value);
            if (req.ToDate.HasValue)
                pred = pred.And(x => x.AppointmentDate <= req.ToDate.Value);
            return pred;
        }

        private Func<IQueryable<TestOrder>, IIncludableQueryable<TestOrder, object>> IncludeAll =>
            q => q
                .Include(x => x.User)
                .Include(x => x.Service)
                    .ThenInclude(s => s.ServiceSampleMethods)
                        .ThenInclude(ssm => ssm.SampleMethod)
                .Include(x => x.SampleMethod)
                .Include(x => x.AppointmentStaff);

        
        public async Task<List<TestOrder>> SearchTestOrdersAsync(SearchTestOrderRequest req)
        {
            var pred = BuildPredicate(req);
            return await base.GetAllAsync(pred, IncludeAll, req.PageIndex, req.PageSize);
        }

       
        public async Task<int> CountTestOrdersAsync(SearchTestOrderRequest req)
        {
            var pred = BuildPredicate(req);
            return await base.CountAsync(pred);
        }
    }


} 