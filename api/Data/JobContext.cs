using JobTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Data;

public class JobContext : DbContext
{
    public JobContext(DbContextOptions<JobContext> options)
        : base(options)
    {
    }

    public DbSet<Job> Jobs => Set<Job>();
}