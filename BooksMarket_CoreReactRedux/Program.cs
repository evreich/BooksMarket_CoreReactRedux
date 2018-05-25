using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.EF.SeedDbHelpers;
using BooksMarket_CoreReactRedux.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BooksMarket_CoreReactRedux
{
    public class Program
    {
        private static async void InitDB(IServiceProvider service)
        {
            await BooksAndGenresSeedData.EnsurePopulated(service);
            await IdentityEntitiesSeedData.EnsurePopulated(service);
        }

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args).MigrateDatabase();
            InitDB(host.Services);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
