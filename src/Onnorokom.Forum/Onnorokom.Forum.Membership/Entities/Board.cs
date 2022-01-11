using Onnorokom.Forum.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Entities
{
    public class Board : IEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required, MaxLength(255)]
        public string BoardName { get; set; }
    }
}
