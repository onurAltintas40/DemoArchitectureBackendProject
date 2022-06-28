

using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorHandlerDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetails : ErrorHandlerDetails
    {
        public IEnumerable<ValidationErrorDetails> Errors { get; set; }
    }
}
