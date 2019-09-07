using HgpStaff3D.Domain.Events;
using HgpStaff3D.Domain.SeedWork;
using System;

namespace HgpStaff3D.Domain.AggregatesModel
{
    public class Department : Entity, IAggregateRoot
    {
        public int EmployeeNumber { get; set; }
        public DateTime Time { get; set; }
        public Organization Organization { get; set; }
        public Guid GroupKey { get; set; }
        public CapUpdate CapUpdated { get; set; }
        public Department() => this.AddDomainEvent(new DepartmentCreatedEvent { Department = this });
    }
}
