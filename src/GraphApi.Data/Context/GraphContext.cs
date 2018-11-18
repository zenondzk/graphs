using GraphApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace GraphApi.Data.Context
{
  public class GraphContext : DbContext, IGraphContext
  {
    IConfiguration configuration;
    public GraphContext(IConfiguration iconfiguration)
    {
      configuration =iconfiguration;
     
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
      optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[]
    {
        new ConsoleLoggerProvider((category, level)
            => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true)
    });

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Edge>()
        .ToTable("Edge");
      builder.Entity<Node>()
        .ToTable("Node");
      
     
      builder.Entity<Graph>()
        .ToTable("Graph")
        .HasKey(x => x.Id);
      builder.Entity<Graph>().HasMany<Node>(x => x.Nodes).WithOne().HasForeignKey(x=>x.GraphId);
      builder.Entity<Graph>().HasMany<Edge>(x => x.Edges).WithOne().HasForeignKey(x=>x.GraphId);
      builder.Entity<Edge>().HasOne<Node>(x => x.ToNode).WithMany().HasForeignKey(x => x.To);
      builder.Entity<Edge>().HasOne<Node>(x => x.FromNode).WithMany().HasForeignKey(x => x.From);
    }

    public DbSet<Graph> Graphs { get; set; }
    public DbSet<Node> Nodes { get; set; }

    public DbSet<Edge> Edges { get; set; }

  }
}
