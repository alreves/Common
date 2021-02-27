using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tgc2e.Common.DataAccess.Ef
{
	public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
	{
		protected string Schema { get; }

		protected EntityTypeConfigurationBase(string schema)
		{
			Schema = schema;
		}

		public abstract void Configure(EntityTypeBuilder<T> builder);
	}
}