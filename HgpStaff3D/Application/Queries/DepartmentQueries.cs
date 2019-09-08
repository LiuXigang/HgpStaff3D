using Dapper;
using HgpStaff3D.CommonExtension;
using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HgpStaff3D.Application.Queries
{
    public class DepartmentQueries: IDepartmentQueries
    {
        private string _constr;
        public DepartmentQueries(string constr) => _constr = constr;
        public async Task<IEnumerable<Department>> GetllAllAsync()
        {
            using (var connection = new SqlConnection(_constr))
            {
                var sql = "SELECT * FROM [dbo].[Departments]";
                var model = await connection.QueryAsync<Department>(sql);
                return model;
            }
        }
        public async Task<ChartData> GetChartDataAsync()
        {
            var list = await GetllAllAsync();
            var data = list.GroupBy(n => n.Organization);
            var charts = new ChartData { Title = "人员变化图" };
            foreach (var item in data)
            {
                var org = item.Key.EnumDesc();
                charts.Legend.Add(org);
                var serieData = new List<int>();
                var xAxis = new List<string>();
                foreach (var depart in item.OrderBy(n => n.Time))
                {
                    xAxis.Add(depart.Time.ToString("MM/dd"));
                    serieData.Add(depart.EmployeeNumber);
                }
                if (!charts.XAxisData.Any())
                    charts.XAxisData.AddRange(xAxis);
                charts.Series.Add(org, serieData);
            }

            var totalStr = "总数";
            charts.Legend.Add(totalStr);
            var totalData = list.OrderBy(n => n.Time).GroupBy(n => n.Time);
            var totalSerie = totalData.Select(item => item.Sum(n => n.EmployeeNumber)).ToList();
            charts.Series.Add(totalStr, totalSerie);
            return charts;
        }
        public async Task<TableModel> GetTableAsync()
        {
            var list = await GetllAllAsync();
            var data = list.OrderBy(n => n.Time).GroupBy(n => n.GroupKey);
            var model = new TableModel();
            model.Org.Add("");
            foreach (int org in Enum.GetValues(typeof(Organization)))
            {
                model.Org.Add(CommExtensions.EnumDesc((Organization)org));
            }
            model.Org.Add("总数");
            foreach (var item in data)
            {
                var rows = new List<string>();
                var i = item.OrderBy(n => n.Time);
                rows.Add(item.Key.ToString());
                rows.Add(i?.FirstOrDefault()?.Time.ToString("yyyy-MM-dd") ?? "");
                rows.AddRange(i.Select(oo => oo.EmployeeNumber.ToString()));
                rows.Add(i.Select(n => n.EmployeeNumber).Sum().ToString());
                model.RowsData.Add(rows);
            }
            return model;
        }
    }
}
