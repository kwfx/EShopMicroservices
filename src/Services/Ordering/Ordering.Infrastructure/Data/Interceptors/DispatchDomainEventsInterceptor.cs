namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null) return;
        var aggregates = context.ChangeTracker.Entries<IAggregate>().Where(e => e.Entity.DomainEvents.Any()).Select(e => e.Entity);
        var allDomainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
        allDomainEvents.ForEach(async e => await mediator.Publish(e));
        aggregates.ToList().ForEach(a => a.ClearDomainEvents());
    }
}