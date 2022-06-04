﻿using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistance.Repositories;

namespace ECommerceAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // Connection string sitesinden postgresql connect'ini bulabiliriz.
            // Daha sonra bu oluşturlan extends fonksiyonu program.cs de çağırıyoruz.
            // EntityFramework.Core.Desing paketini kuruyoruz ancak defaultta persistance'a ayarlıyoruz. Kurulum yeri olarak da API Layer'a. (Migration basmak için.)
            // add-migration mig_1 diyerek migration oluşturuyoruz.
            // Database güncellemek için Database-Update
            // Appsettings'e yazdığımız postgresql connectionu kullanmak için microsoft.configuration paketini yüklüyoruz.
            // Daha sonra configuration.json paketini yüklüyoruz.
            // Eğer appsettings ile işimiz çok ise bunu sınıf haline getirip Configurations altında yaptım bunu bu şekilde sürdürebiliriz.
            // RepositoryDI larını eklerken DbContext'e göre addscoped yada addsingelton olarak eklersek daha hatasız hareket etmiş oluruz. ( AddSingelton aynı zamanda bir antipattern'dir.)


            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configurations.ConnectionString));
            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository,OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();
            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
            
        }
    }
}
