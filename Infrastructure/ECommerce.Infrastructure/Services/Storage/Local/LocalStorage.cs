﻿using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{

    // Eğer ki ilgili uygulamanın bulundugu sunucuda dosya ekleme işlemi yapacaksak local storage kullanacağız.
    public class LocalStorage : Storage,ILocalStorage
    {
        // Burda bir hata ile karşılaştık bunu bulmak da çok kolay olmadı benim için
        // Yapmamız gereken Framework olarak Microsoft.AspNetCore.Hosting yapısını eklemek.

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
            
            =>  File.Delete($"{path}\\{fileName}");
        

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                // IDisposible dan türediği için using kullanırsak kendini otomatik olarak dispose eder.
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {   // todo log!
                throw ex;
            }


        }


        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            foreach (IFormFile file in files)
            {

                string newName =  await FileRenameAsync(file.Name);

                await CopyFileAsync($"{uploadPath}\\{newName}", file);
                datas.Add((newName, $"{path}\\{newName}"));
                
            }

            

            return datas;
        }
    }
}
