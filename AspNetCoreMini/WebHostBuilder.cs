using System;
using System.Collections.Generic;

namespace AspNetCoreMini
{
    /// <summary>
    /// 宿主构建器
    /// </summary>
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        IWebHost Build();
    }

    /// <summary>
    /// 宿主构建器
    /// </summary>
    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer _server;
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        /// <summary>
        /// 注册Server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }

        /// <summary>
        /// 构建WebHost
        /// </summary>
        /// <returns></returns>
        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (Action<IApplicationBuilder> configure in _configures)
            {
                configure(builder);
            }
            RequestDelegate handler = builder.Build();
            return new WebHost(_server, handler);
        }
    }
}
