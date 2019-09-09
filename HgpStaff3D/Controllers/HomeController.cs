using HgpStaff3D.Application.MediatRCommands;
using HgpStaff3D.Application.Queries;
using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Infrastructure.Repositories;
using HgpStaff3D.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HgpStaff3D.Controllers
{
    /// <summary>
    /// Home控制器
    /// </summary>
    public class HomeController : Controller
    {
        private IMediator _mediatR;
        private IDepartmentRepository _repository;
        private IDepartmentQueries _query;
        /// <summary>
        /// DI
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        /// <param name="query"></param>
        public HomeController(IMediator mediator, 
            IDepartmentRepository repository, 
            IDepartmentQueries query)
        {
            _mediatR = mediator;
            _repository = repository;
            _query = query;
        }

        /// <summary>
        /// 折线图页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取折线图数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var charts = await _query.GetChartDataAsync();
            return Json(charts);
        }
        /// <summary>
        /// 添加数据页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            var model = new AddDepartmentsModel { Time = DateTime.Now.Date };
            return View(model);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentsModel model)
        {
            var groupKey = Guid.NewGuid();
            model.Time = model.Time.Date;
            var departments = new List<Department>
            {
                new Department {CapUpdated=CapUpdate.UnUpdate, GroupKey=groupKey, Organization = Organization.Ceo, Time = model.Time, EmployeeNumber = model.CeoNo },
                new Department { CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey,Organization = Organization.InternalControl, Time = model.Time, EmployeeNumber = model.InternalControlNo },
                new Department { CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey,Organization = Organization.StrategicInvestment, Time = model.Time, EmployeeNumber = model.StrategicInvestmentNo },
                new Department {CapUpdated=CapUpdate.UnUpdate, GroupKey=groupKey,Organization = Organization.HumanResources, Time = model.Time, EmployeeNumber = model.HumanResourcesNo },
                new Department {CapUpdated=CapUpdate.UnUpdate, GroupKey=groupKey,Organization = Organization.FinancialManagement, Time = model.Time, EmployeeNumber = model.FinancialManagementNo },
                new Department { CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey,Organization = Organization.ProductDevelopment, Time = model.Time, EmployeeNumber = model.ProductDevelopmentNo },
                new Department { CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey,Organization = Organization.SupplyChain, Time = model.Time, EmployeeNumber = model.SupplyChainNo },
                new Department {CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey, Organization = Organization.UserCenter, Time = model.Time, EmployeeNumber = model.UserCenterNo },
                new Department { CapUpdated=CapUpdate.UnUpdate,GroupKey=groupKey,Organization = Organization.ExternalContact, Time = model.Time, EmployeeNumber = model.ExternalContactrNo }
            };
            var command = new CreateDepartmentCommand { Departments = departments };
            await _mediatR.Send(command, new CancellationToken());
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Table()
        {
            var model = await _query.GetTableAsync();
            return View(model);
        }
        /// <summary>
        /// 删除某天数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid groupKey)
        {
            await _repository.DeleteByGroupKeyAsync(groupKey);
            await _repository.UnitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
    }
}
