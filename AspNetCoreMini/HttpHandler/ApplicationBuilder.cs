using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreMini
{
    /// <summary>
    /// 应用构建器
    /// （即HttpHandler构建器，而HttpHandler又是用RequestDelegate表示的）
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        /// <summary>
        /// 构建Application
        /// (即HttpHandler，以RequestDelegate表示)
        /// </summary>
        /// <returns></returns>
        RequestDelegate Build();
    }

    /// <summary>
    /// 应用构建器
    /// （即HttpHandler构建器，而HttpHandler又是用RequestDelegate表示的）
    /// </summary>
    public class ApplicationBuilder : IApplicationBuilder
    {
        /// <summary>
        /// 中间件集合
        /// </summary>
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 构建Application
        /// (即HttpHandler，以RequestDelegate表示)
        /// </summary>
        /// <returns></returns>
        public RequestDelegate Build()
        {
            _middlewares.Reverse();
            RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
            foreach (Func<RequestDelegate, RequestDelegate> middleware in _middlewares)
            {
                next = middleware(next);
            }
            return next;
        }

        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
