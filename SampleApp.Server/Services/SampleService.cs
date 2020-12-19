using MagicOnion;
using MagicOnion.Server;
using SampleApp.Shared.Services;

namespace SampleApp.Server.Services 
{
    public class SampleService : ServiceBase<ISampleService>, ISampleService
    {
        public UnaryResult<int> SumAsync(int x, int y) => UnaryResult(x + y);
    }
}