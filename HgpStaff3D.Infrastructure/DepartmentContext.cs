using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Domain.SeedWork;
using HgpStaff3D.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HgpStaff3D.Infrastructure
{
    public class DepartmentContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DbSet<Department> Departments { get; set; }
        private DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options) { }
        public DepartmentContext(DbContextOptions<DepartmentContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Debug.WriteLine("DepartmentContext::ctor ->" + this.GetHashCode());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DispatchDomainEventsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return true;
        }
    }
}
