using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer.Context;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)	{}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Information> Information { get; set; }
    public DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
}
