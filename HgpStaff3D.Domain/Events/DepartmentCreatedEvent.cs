using HgpStaff3D.Domain.AggregatesModel;
using MediatR;

namespace HgpStaff3D.Domain.Events
{
    public class DepartmentCreatedEvent : INotification
    {
        public Department Department { get; set; }
    }
}
