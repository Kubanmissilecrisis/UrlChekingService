using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.Models;

namespace WebCheckerAPI.EntityFrameworkStuff
{
	public class RequestDbContext : DbContext
	{
		public RequestDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<WebCheckerAPI.Models.Endpoint> Endpoints { get; set; } = null!;
		public DbSet<EndpointResult> EndpointResults { get; set; } = null!;
	}
}