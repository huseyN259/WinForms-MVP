using Microsoft.EntityFrameworkCore;
using Source.Models;

namespace Source.Repositories;

public class MyDbContext : DbContext
{
    //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = 'DemoMVP';Integrated Security = True;

    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        dbContextOptionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = DemoMVP;Integrated Security = True;");

        base.OnConfiguring(dbContextOptionsBuilder);
    }

    public DbSet<Student>? Students { get; set; }
}