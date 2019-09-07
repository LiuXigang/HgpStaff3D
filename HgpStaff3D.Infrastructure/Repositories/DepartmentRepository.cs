using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace HgpStaff3D.Infrastructure.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DepartmentContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public DepartmentRepository(DepartmentContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Department>> AddRangeAsync(List<Department> models)
        {
            var departments = models.Where(n => n.IsTransient());
            await _context.Departments.AddRangeAsync(departments);
            return departments;
        }
        public async Task<Department> GetAsync(int id)
        {
            return await _context.FindAsync<Department>(id);
        }
        public async Task DeleteByGroupKeyAsync(Guid groupKey)
        {
            var models = await _context.Departments.Where(n => n.GroupKey == groupKey).ToListAsync();
            _context.Departments.RemoveRange(models);
        }
    }
}
