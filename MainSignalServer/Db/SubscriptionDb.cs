using Microsoft.EntityFrameworkCore;

namespace MainSignalServer.Models;

public class SubscriptionDb:DbContext
{
    public SubscriptionDb(DbContextOptions options) : base(options) { }
    public DbSet<SubRequest> Subscriptions { get; set; } = null!;
}