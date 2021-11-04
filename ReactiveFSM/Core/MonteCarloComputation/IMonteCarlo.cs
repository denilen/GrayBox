using System.Threading.Tasks;

namespace ReactiveFSM.Core.MonteCarloComputation
{
    public interface IMonteCarlo
    {
        public abstract Task<double> Computation(object obj);
    }
}