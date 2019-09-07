using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HgpStaff3D.Application.Queries
{
    public interface IDepartmentQueries
    {
        Task<IEnumerable<Department>> GetllAllAsync();
        Task<ChartData> GetChartDataAsync();
        Task<TableModel> GetTableAsync();
    }
}
