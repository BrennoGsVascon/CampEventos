using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CampEventos.API.Helpers
{
    public class Util : IUtil
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public Util(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> SaveImage(IFormFile imageFile, string destino)
        {
            var imageName = new string(
                Path.GetFileNameWithoutExtension(imageFile.FileName)
                    .Take(10)
                    .ToArray()
            ).Replace(' ', '-');

            imageName =
                $"{imageName}{DateTime.UtcNow:yyMMddHHmmssfff}{Path.GetExtension(imageFile.FileName)}";

            var folderPath = Path.Combine(
                _hostEnvironment.ContentRootPath,
                "Resources",
                destino
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var imagePath = Path.Combine(folderPath, imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public void DeleteImage(string imageName, string destino)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return;

            var imagePath = Path.Combine(
                _hostEnvironment.ContentRootPath,
                "Resources",
                destino,
                imageName
            );

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}