﻿using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();


        }
        // IStorage'dan türemiş sınıf olmasını şart koşacağız.
        public static void AddStorage<T>(this IServiceCollection serviceCollection)  where T : class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }


    }
}
