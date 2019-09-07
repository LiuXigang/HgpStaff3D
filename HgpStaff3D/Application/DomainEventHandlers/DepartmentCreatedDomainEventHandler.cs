using DotNetCore.CAP;
using HgpStaff3D.Application.IntergrationEvents;
using HgpStaff3D.Domain.Events;
using HgpStaff3D.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HgpStaff3D.Application.DomainEventHandlers
{
    public class DepartmentCreatedDomainEventHandler : INotificationHandler<DepartmentCreatedEvent>
    {
        private ICapPublisher _capPublisher;
        public DepartmentCreatedDomainEventHandler(ICapPublisher capPublisher) => _capPublisher = capPublisher;
        public Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification?.Department is null)
                throw new DomainException($"DepartmentCreatedDomainEventHandler-notification not found！");

            var capEvent = new DepartmentCreatedIntergrationEvent { DepartmentId = notification.Department.Id };
            _capPublisher.Publish("HgpStaff3D.DepartmentCreated", capEvent);
            return Task.CompletedTask;
        }
    }
}
