using System;
using Microsoft.Extensions.Configuration;

namespace Tgc2e.Common.Configuration
{
	public static class ConfigurationLoader
	{
		private static readonly Lazy<IConfiguration> Loader = new Lazy<IConfiguration>(() => new ConfigurationBuilder()
			.AddJsonFile($"appsettings.json")
			.Build());

		public static IConfiguration Configuration => Loader.Value;
	}
}
