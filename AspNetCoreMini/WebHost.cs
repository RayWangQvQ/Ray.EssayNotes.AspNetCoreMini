using System.Threading.Tasks;

namespace AspNetCoreMini
{
    /// <summary>
    /// 承载Web应用的宿主
    /// </summary>
    public interface IWebHost
    {
        Task StartAsync();
    }

    /// <summary>
    /// 承载Web应用的宿主
    /// </summary>
    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;
        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }
        public Task StartAsync() => _server.StartAsync(_handler);
    }
}
