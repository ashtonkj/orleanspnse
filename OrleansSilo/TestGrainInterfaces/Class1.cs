using Orleans;
using System;
using System.Threading.Tasks;

namespace TestGrainInterfaces
{
    public interface ITestGrain: IGrainWithGuidKey
    {
        Task<int> Test();
    }
}
