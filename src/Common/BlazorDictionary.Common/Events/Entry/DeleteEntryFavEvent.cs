namespace BlazorDictionary.Common.Events.Entry
{
    public class DeleteEntryFavEvent
    {
        public Guid EntryId { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
