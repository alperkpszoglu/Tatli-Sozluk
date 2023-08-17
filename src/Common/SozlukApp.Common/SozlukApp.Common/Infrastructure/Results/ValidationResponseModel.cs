using System.Text.Json.Serialization;

namespace SozlukApp.Common.Infrastructure.Results
{
    public class ValidationResponseModel
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationResponseModel(IEnumerable<string> Errors)
        {
                this.Errors = Errors;
        }

        public ValidationResponseModel(string errorMessage): this(new List<string>() { errorMessage})
        {

        }

        [JsonIgnore]
        public string FlattenErrors => Errors != null
            ? string.Join(Environment.NewLine, Errors) 
            : string.Empty;

    }
}
