using System.Threading.Tasks;

namespace ReactiveStateless.Core.MonteCarloComputation
{
    public class MonteCarlo : IMonteCarlo
    {
        public async Task<double> Computation(object obj)
        {
            await Task.Delay(1000);

            return 1;
        }
    }
}