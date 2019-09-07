using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HgpStaff3D.Infrastructure.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> AddRangeAsync(List<Department> models);
        Task<Department> GetAsync(int id);
        Task DeleteByGroupKeyAsync(Guid groupKey);
    }
}
