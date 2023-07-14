using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Domain.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EmailConfirmed { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }

        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }


    }
}
