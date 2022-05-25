namespace BlazorDictionary.Api.Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
