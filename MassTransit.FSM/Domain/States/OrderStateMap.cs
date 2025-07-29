using MassTransit;
// using MassTransit.EntityFrameworkCoreIntegration;
// using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransit.FSM.Domain.States;

public class OrderStateMap : SagaClassMap<OrderState>
{
    protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState);
        entity.Property(x => x.SubmittedAt);
        entity.Property(x => x.AcceptedAt);
        entity.Property(x => x.CompletedAt);
    }
}
