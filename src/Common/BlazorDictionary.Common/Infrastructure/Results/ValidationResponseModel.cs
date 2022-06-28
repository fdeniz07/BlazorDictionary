using System.Text.Json.Serialization;

namespace BlazorDictionary.Common.Infrastructure.Results
{
    public class ValidationResponseModel 
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ValidationResponseModel(string message):this(new List<string>() { message})
        {

        }

        [JsonIgnore] // Bu property'i disariya dönmemek icin bu attribute kullanilir
        public string FlattenErrors => Errors != null
            ? string.Join(Environment.NewLine, Errors)
            :string.Empty;
    }
}
