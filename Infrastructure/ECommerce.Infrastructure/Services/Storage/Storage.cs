using ECommerceAPI.Infrastructure.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage 
    {
        // Bu sınıf'a niye ihtiyaç duyduğumuzu açıklayayım.
        // Interface'i implemente edip farklı storage'a göre farklı davranışlar sergileyecek yapılar IStorage interface'ni implemente edecek.
        // Ancak bu yapılardaki davranış değişikli aynı olan yapıyı ise Storage sınıfı sağlayacak. Örnek : RenameAsync();.




        // Sadece kalıtımsal olarak erişilsin.
        protected async Task<string> FileRenameAsync(string fileName)
        {
            string newFileName = await Task.Run<string>(() => {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.ChracterRegulatory(fileName)}-{DateTime.Now}{extension}";

                return newFileName;

            });

            return newFileName;
        }


    }
}
