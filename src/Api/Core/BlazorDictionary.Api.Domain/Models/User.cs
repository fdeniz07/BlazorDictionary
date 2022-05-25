namespace BlazorDictionary.Api.Domain.Models
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool EmailConfirmed { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }

        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }

        public virtual ICollection<EntryComment> EntryComments { get; set; }
        
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    }
}
