using ITL.NetCore.Connection.Caching;
using ITL.NetCore.Connection.EF;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using eTMS.API.Common.Globals;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
namespace FileManagementAPI.API.Infrastructure
{
    public static class ServiceRegister
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Register Redis service with specific connection
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
            
        }
    }
}
