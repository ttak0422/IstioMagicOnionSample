using MagicOnion;

namespace SampleApp.Shared.Services
{
    public interface ISampleService : IService<ISampleService>
    {
        UnaryResult<int> SumAsync(int x, int y);
    }
}