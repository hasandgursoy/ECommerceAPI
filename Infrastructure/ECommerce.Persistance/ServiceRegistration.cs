using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace ECommerceAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // Connection string sitesinden postgresql connect'ini bulabiliriz.
            // Daha sonra bu oluşturlan extends fonksiyonu program.cs de çağırıyoruz.
            // EntityFramework.Core.Desing paketini kuruyoruz ancak defaultta persistance'a ayarlıyoruz. Kurulum yeri olarak da API Layer'a. 
            // add-migration mig_1 diyerek migration oluşturuyoruz.
            // Database güncellemek için Database-Update
            // Appsettings'e yazdığımız postgresql connectionu kullanmak için microsoft.configuration paketini yüklüyoruz.
            // Daha sonra configuration.json paketini yüklüyoruz.
            // Eğer appsettings ile işimiz çok ise bunu sınıf haline getirip Configurations altında yaptım bunu bu şekilde sürdürebiliriz.
            
            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configurations.ConnectionString));
        }
    }
}
