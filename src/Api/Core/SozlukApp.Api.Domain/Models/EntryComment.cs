using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Domain.Models
{
    public class EntryComment: BaseEntity
    {
        public string Content { get; set; }

        public Guid CreatedById { get; set; }

        public Guid EntryId { get; set; }

        // navigation props
        public virtual Entry Entry{ get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }

    }
}
