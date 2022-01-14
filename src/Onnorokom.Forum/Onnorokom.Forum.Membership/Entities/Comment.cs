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
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        [ForeignKey("Post")]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        [Required, MaxLength(512)]
        public string CommentText { get; set; }
        [Required, MaxLength(255)]
        public string CreatorEmail { get; set; }
        [ForeignKey("ApplicationUser")]
        public Guid CreatorId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}