using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public interface IServer
    {
        /// <summary>
        /// 启动Server
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        Task StartAsync(RequestDelegate handler);
    }
}
