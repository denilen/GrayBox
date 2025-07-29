using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.FSM.Domain.States;
using Microsoft.EntityFrameworkCore;

namespace MassTransit.FSM.Infrastructure.Persistence;

public class OrderStateDbContext : SagaDbContext
{
    public OrderStateDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new OrderStateMap(); }
    }
}
