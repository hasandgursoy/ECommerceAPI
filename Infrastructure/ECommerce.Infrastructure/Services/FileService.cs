using ECommerceAPI.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService 
    {
        



        
        // Bu fonksiyon normalde IFileService'de vardı ancak biz onu sadece burada kullanacağımız için 
        // Ve başkalarıyla paylaşma ihtiyacımız olmadığı için kaldırdık.
        public async Task<string> FileRenameAsync(string path, string fileName )
        {
            string newFileName = await Task.Run<string>(async () => {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.ChracterRegulatory(fileName)}-{DateTime.Now}{extension}";

                return newFileName;

            });

            return newFileName;
        }


    }
}


// dosya yüklemek için gerekli olan yapının temelini kuruyoruz. eski yapıyı productscontrollerda de kurmustuk 
// eski kodları burada aşşağıda yorum satırı olarak yazacağım.


// Eski kodlar

/*
 public async Task<IActionResult> Upload()
        {   
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");
            
            // Eğer böyle bir path yoksa bu path'i yarat diyoruz.
            if(!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

            // Form Data'dan gelen istekleri yakalamaya yardım ediyor.
            Random r = new();
            // Files olarak gelecek ve buda IFormCollection yapısına sahip bir yapı biz bunu for ile dönebiliyoruz. 
            foreach (IFormFile file in Request.Form.Files)
            {
                string v = Path.GetExtension(file.FileName);
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{v}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync:false);
                // Hedef Path'e bu değeri basıyoruz.
                await file.CopyToAsync(fileStream);
                // Daha sonrada stream'i boşaltırıyoruz.
                await fileStream.FlushAsync();
                
            }

            return Ok();
        }
 */