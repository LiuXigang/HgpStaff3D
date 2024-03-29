﻿using MediatR;
using System.Collections.Generic;

namespace HgpStaff3D.Domain.SeedWork
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        public virtual int Id { get; protected set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id == 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; 
                return _requestedHashCode.Value;
            }

            return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            return left?.Equals(right) ?? right == null;
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
