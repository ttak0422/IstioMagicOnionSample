using Grpc.Core;
using MagicOnion;
using MagicOnion.Hosting;
using MagicOnion.HttpGateway.Swagger;
using MagicOnion.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace SampleApp.Server 
{
    class Program
    {
        private const string Host =
#if DEBUG
            "localhost";
#else
            "0.0.0.0";
#endif
        static readonly string MagicOnionPort = Environment.GetEnvironmentVariable("MAGICONION_PORT") ?? "12345";
        static readonly string SwaggerPort = Environment.GetEnvironmentVariable("SWAGGER_PORT") ?? "5432";

        static async Task Main(string[] _)
        {
            var magicOnionHost =
                MagicOnionHost.CreateDefaultBuilder()
                    .UseMagicOnion(
                        new MagicOnionOptions(),
                        new ServerPort
                            ( Host
                            , int.Parse(MagicOnionPort)
                            , ServerCredentials.Insecure
                            )
                    )
                    .UseConsoleLifetime()
                    .Build();

            var webHost =
                new WebHostBuilder()
                    .ConfigureServices(collection =>
                    {
                        collection.AddSingleton
                            (magicOnionHost.Services.GetService<MagicOnionHostedServiceDefinition>()?.ServiceDefinition!);
                    })
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .UseUrls($"http://{Host}:{SwaggerPort}")
                    .Build();

            await Task.WhenAll(
                webHost.RunAsync(), 
                magicOnionHost.RunAsync()
            );
        }

        public class Startup 
        {
            public void Configure(IApplicationBuilder app, MagicOnionServiceDefinition magicOnion) {
                app.UseMagicOnionSwagger
                    ( magicOnion.MethodHandlers
                    , new SwaggerOptions
                        ( "SampleApp.Server"
                        , "Swaggerr"
                        , "/"
                        ) {}
                    );
                app.UseMagicOnionHttpGateway
                    ( magicOnion.MethodHandlers
                    , new Channel
                        ( $"localhost:{MagicOnionPort}"
                        , ChannelCredentials.Insecure
                        )
                    );
            }
        }
    }
}

