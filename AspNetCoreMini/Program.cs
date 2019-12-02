using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public class Program
    {
        public static async Task Main()
        {
            /*
            await new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware))
                .Build()
                .StartAsync();
            */


            //1.生成webHost构建器
            var webHostBuilder = new WebHostBuilder();
            //2.注册Server
            webHostBuilder.UseHttpListener();
            //3.添加中间件
            webHostBuilder.Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware));
            //4.构建WebHost
            IWebHost webHost = webHostBuilder.Build();
            //5.启动
            await webHost.StartAsync();
        }


        public static RequestDelegate FooMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Foo=>");
                await next(context);
            };

        public static RequestDelegate BarMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Bar=>");
                await next(context);
            };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
            => context => context.Response.WriteAsync("Baz");
    }
}