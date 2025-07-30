using System.Threading.Tasks;

namespace ReactiveStateless.Core.MonteCarloComputation;

public interface IMonteCarlo
{
    public abstract Task<double> Computation(object obj);
}
