using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreMini.Features;

namespace AspNetCoreMini
{
    /// <summary>
    /// Http监听Server
    /// </summary>
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;
        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            Console.WriteLine("Server started and is listening on: {0}", string.Join(';', _urls));

            while (true)
            {
                //1.监听到Http请求，生成自己的上下文
                HttpListenerContext listenerContext = await _httpListener.GetContextAsync();

                //2.生成feature适配
                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);

                //3.由feature生成HttpContext
                var httpContext = new HttpContext(features);

                //4.进入HttpHandler
                await handler(httpContext);

                //5.结束
                listenerContext.Response.Close();
            }
        }
    }

    public static partial class Extensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
        => builder.UseServer(new HttpListenerServer(urls));
    }
}
