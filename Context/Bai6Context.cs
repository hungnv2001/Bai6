using Bai6.Model;
using Microsoft.EntityFrameworkCore;

namespace Bai6.Context
{
	public class Bai6Context : DbContext
	{
	
		public Bai6Context()
		{
		}

		public Bai6Context(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Reservation>? Reservations { get; set; }
	}
}
