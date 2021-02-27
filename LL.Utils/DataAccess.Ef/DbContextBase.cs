using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tgc2e.Common.DataAccess.Ef
{
	public abstract class DbContextBase : DbContext, IDbContext
	{
		private static readonly IConfigurationRoot ConfigurationRoot;

		protected string SchemaName { get; }

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		static DbContextBase()
		{
			var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", false);
			ConfigurationRoot = builder.Build();
		}

		protected DbContextBase(string schemaName)
		{
			SchemaName = schemaName;
		}

		protected string GetConnectionString(string connectionStringName)
		{
			return ConfigurationRoot.GetConnectionString(connectionStringName);
		}
	}
}