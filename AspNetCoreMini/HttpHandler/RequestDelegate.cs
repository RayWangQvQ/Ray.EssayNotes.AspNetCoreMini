using System.Threading.Tasks;

namespace AspNetCoreMini
{
    /// <summary>
    /// 相当于HttpHandler
    /// 即已注册的所有中间件的整个执行委托
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task RequestDelegate(HttpContext context);
}
