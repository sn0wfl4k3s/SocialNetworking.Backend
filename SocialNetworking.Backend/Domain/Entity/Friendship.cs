using Core.Domain;
using System;

namespace Domain.Entity
{
    public class Friendship : IEntity
    {
        public ulong Id { get; set; }
        public virtual User From { get; set; }
        public virtual User To { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
    }
}
