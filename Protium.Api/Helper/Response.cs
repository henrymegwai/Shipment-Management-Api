namespace Protium.Api.Helper
{
    public class Response<T> where T : class
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; } 
    }
}
