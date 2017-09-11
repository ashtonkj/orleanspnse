using Orleans;
using System;
using System.Threading.Tasks;
using TestGrainInterfaces;

namespace TestGrains
{
    public class TestGrain : Grain,ITestGrain
    {
        

        public Task<int> Test()
        {
            return Task.FromResult(1);
        }
    }
}
