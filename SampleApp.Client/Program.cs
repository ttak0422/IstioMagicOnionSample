using Grpc.Core;
using MagicOnion.Client;
using System;
using System.Threading.Tasks;
using SampleApp.Shared.Services;

namespace SampleApp.Client
{
    class Program
    {
        static readonly string Host = Environment.GetEnvironmentVariable("GRPC_HOST") ?? "localhost";
        static readonly string Port = Environment.GetEnvironmentVariable("GRPC_PORT") ?? "12345";
        
        static async Task Main(string[] _)
        {
            var channel = new Channel
                    ( Host
                    , int.Parse(Port)
                    , ChannelCredentials.Insecure
                    );
            var client = MagicOnionClient.Create<ISampleService>(channel);

            while (true)
            {
                Console.WriteLine("input x y (e.g. 1 2).");
                var inputs = Console.ReadLine().Split();
                if (inputs.Length < 2) continue;
                var isValidX = int.TryParse(inputs[0], out var x);
                var isValidY = int.TryParse(inputs[1], out var y);
                if(isValidX && isValidY)
                {
                    var sum = await client.SumAsync(x, y);
                    Console.WriteLine($"sum of x y is {sum}.");
                } else
                {
                    Console.WriteLine("invalid.");
                }
                Console.WriteLine();
            }
        }
        
    }
}
