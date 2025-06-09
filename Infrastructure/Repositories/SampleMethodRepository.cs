using Application.Repository;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SampleMethodRepository : GenericRepository<SampleMethod>, ISampleMethodRepository
    {
        public SampleMethodRepository(AppDbContext context) : base(context)
        {
        }
    }
} 