namespace MinerthalSalesApp.Models
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}
