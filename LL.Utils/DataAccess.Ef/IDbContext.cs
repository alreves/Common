using System;
using System.Threading.Tasks;

namespace Tgc2e.Common.DataAccess.Ef
{
	public interface IDbContext : IDisposable
	{
		Task<int> SaveChangesAsync();
	}
}