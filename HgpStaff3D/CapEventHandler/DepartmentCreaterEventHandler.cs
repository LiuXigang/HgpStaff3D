using DotNetCore.CAP;
using HgpStaff3D.Application.IntergrationEvents;
using HgpStaff3D.Domain.AggregatesModel;
using HgpStaff3D.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace HgpStaff3D.CapEventHandler
{
    public class DepartmentCreaterEventHandler : ICapSubscribe
    {
        private IDepartmentRepository _repository;
        public DepartmentCreaterEventHandler(IDepartmentRepository repository) => _repository = repository;

        [CapSubscribe("HgpStaff3D.DepartmentCreated")]
        public async Task UpdateDepartment(DepartmentCreatedIntergrationEvent @event)
        {
            if (@event is null)
                return;
            var model = await _repository.GetAsync(@event.DepartmentId);
            model.CapUpdated = CapUpdate.Updated;
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
