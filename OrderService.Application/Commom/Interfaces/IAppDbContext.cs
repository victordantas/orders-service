using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

public interface IAppDbContext
{
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}