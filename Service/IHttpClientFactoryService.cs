namespace Service
{
    public interface IHttpClientFactoryService
    {
        string Metodo { get; set; }

        Dictionary<string, object> Parametros { get; set; }

        TimeSpan? Timeout { get; set; }

        Task<TResult> InvocarGetAsync<TResult>();

        Task<byte[]> InvocarGetAsync();

        Task<TResult> InvocarPostAsJsonAsync<TResult>();

        Task<byte[]> InvocarPostAsJsonAsync();
    }
}
