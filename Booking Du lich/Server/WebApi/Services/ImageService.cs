using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        // áp dụng cho upload city
        // mỗi city chỉ có 1 ảnh duy nhất nên không cần phân chia thư mục để chứa nhiều ảnh cho 1 city
        // add 1 ảnh vào 1 thư mục
        public async Task<bool> AddOneToFolder(IFormFile file, string folder)
        {
            bool result = false;
            try
            {
                string fileName = file.FileName;
                string filePath = GetFilePath(fileName, folder);

                // kiểm tra đường dẫn thư đã tồn tại chưa
                // nếu chưa thì tạo mới
                //if(System.IO.Directory.Exists(filePath) == false)
                //{
                //    System.IO.Directory.CreateDirectory(filePath);
                //}

                // nếu đã tồn tại đường dẫn file trước đó thì xóa file cũ
                if (System.IO.File.Exists(filePath) == true)
                {
                    System.IO.File.Delete(filePath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public string GetFilePath(string fileName, string folder)
        {
            var imagesPath = Path.Combine(hostEnvironment.WebRootPath, "images");
            var folderPath = Path.Combine(imagesPath, folder);
            var filePath = Path.Combine(folderPath, fileName);
            return filePath;
        }
    }
}
