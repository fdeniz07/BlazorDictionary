namespace BlazorDictionary.Common.Models.Queries
{
    public class BaseFooterRateViewModel
    {
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavoritedViewModel
    {
        public bool IsFavorited { get; set; }

        public int FavoritedCount { get; set; }
    }

    public class BaseFooterRateFavoritedViewModel : BaseFooterFavoritedViewModel
    {
        public VoteType VoteType { get; set; }
    }
}
