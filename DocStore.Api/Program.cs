﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace DocStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "DocStore";

            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                                                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                                  .MinimumLevel.Override("System", LogEventLevel.Warning)
                                                  .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                                                  .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day,
                                outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateLogger();


            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
                        WebHost.CreateDefaultBuilder(args)
                               .UseStartup<Startup>()
                               .UseSerilog()
                               .Build();
    }
}
