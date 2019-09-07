using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Domain.Exceptions;
using HgpStaff3D.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HgpStaff3D.Application.MediatRCommands
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, IEnumerable<Department>>
    {
        private IDepartmentRepository _repository;
        public CreateDepartmentHandler(IDepartmentRepository repository) => _repository = repository;
        public async Task<IEnumerable<Department>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if(request?.Departments is null)
                throw new DomainException($"CreateDepartmentHandler-request not found！");

            var result = await _repository.AddRangeAsync(request.Departments);
            await _repository.UnitOfWork.SaveChangesAsync();
            await _repository.UnitOfWork.DispatchDomainEventsAsync();
            return result;
        }
    }
}
