using Microsoft.EntityFrameworkCore;

public class ConectionDB : DbContext
{
    public ConectionDB(DbContextOptions<ConectionDB> options)
        : base(options)
    {
    }

    // Define tus DbSets aquí, por ejemplo:
    // public DbSet<YourEntity> YourEntities { get; set; }
}

