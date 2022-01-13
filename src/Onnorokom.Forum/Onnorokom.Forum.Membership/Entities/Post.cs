using Onnorokom.Forum.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Entities
{
    public class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required, MaxLength(1024)]
        public string Description { get; set; }
        [ForeignKey("Topic")]
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
        public Guid CreatorId { get; set; }
        [Required, MaxLength(255)]
        public string CreatorEmail { get; set; }
    }
}
