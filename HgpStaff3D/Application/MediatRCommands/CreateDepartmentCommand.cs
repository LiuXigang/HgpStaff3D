using HgpStaff3D.Domain.AggregatesModel;
using MediatR;
using System.Collections.Generic;

namespace HgpStaff3D.Application.MediatRCommands
{
    public class CreateDepartmentCommand : IRequest<IEnumerable<Department>>
    {
        public List<Department> Departments { get; set; }
    }
}
