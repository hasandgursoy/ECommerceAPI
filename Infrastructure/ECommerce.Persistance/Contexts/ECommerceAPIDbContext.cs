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
        
        // Burdan aşşağısı Table-Per-Hierarchy çalışmasıdır File dosyasını miras alıcaklar sonrada gerisini efcore halledecek.
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını
            // sağlayan propertydir. Update Operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.
            // Aşşğaıda neden base entity verdik çünkü diğer entityler onu miras alıyor bu şekilde evrensel bir yapı kurduk.
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {   // _ alocation işlemi yapma diyoruz
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    // Delete EndPoint'in de veri silindiği için DateTime.UtcNow atanamıyor bu da hata veriyor.
                    // o yüzden bu iki değilse normal hiç kullanmayacağımız bir veri dönelim
                    _ => DateTime.UtcNow
                };

            }
            return base.SaveChangesAsync(cancellationToken);


        }
    }
}