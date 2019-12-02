using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace TestAspNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            new WebHostBuilder()
                .UseKestrel()
                .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
                .Build()
                .Run();
                */

            //1.生成WebHostBuilder
            var webHostBuilder = new WebHostBuilder();
            //2.注册Server
            webHostBuilder.UseKestrel();
            //3.添加中间件
            webHostBuilder.Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")));
            //4.构建WebHost
            IWebHost webHost = webHostBuilder.Build();
            //5.启动
            webHost.Run();
        }
    }
}
