namespace API.Dtos
{
    public class ResponseData<T> where T : class
    {
        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public List<T> Data { get; set; }

        public int Count { get; set; }
    }
}
