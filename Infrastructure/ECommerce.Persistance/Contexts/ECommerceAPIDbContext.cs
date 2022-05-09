using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Contexts
{
    public class ECommerceAPIDbContext : DbContext
    {
        public ECommerceAPIDbContext(DbContextOptions options) : base(options)
        {
            // IOC 'de dolduralacak.
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını
            // sağlayan propertydir. Update Operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.
            // Aşşğaıda neden base entity verdik çünkü diğer entityler onu miras alıyor bu şekilde evrensel bir yapı kurduk.
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                data.State state
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}

