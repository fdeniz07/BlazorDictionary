namespace BlazorDictionary.Common.Models.Queries
{
    public class GetEntryDetailViewModel:BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedByUserName { get; set; }
    }
}
