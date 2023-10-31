using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string[] GetAllFileOfFolder(params string[] folder)
        {
            // lấy đường dẫn dự án đến folder chứa ảnh
            var folderAbsolute = GetPath(folder);

            if (Directory.Exists(folderAbsolute) == false)
            {
                return null;
            }

            // lấy đường dẫn tuyệt đối của tất cả file trong folder chứa ảnh
            var filePathAbsolutes = Directory.GetFiles(folderAbsolute);

            // tạo mảng lưu đường dẫn tương đối của ảnh
            int n = filePathAbsolutes.Length;
            string[] fileNames = new string[n];

            string folderRelative = Path.Combine(folder);

            for (int i = 0; i < n; i++)
            {
                string fileName = Path.GetFileName(filePathAbsolutes[i]);
                fileNames[i] = Path.Combine(folderRelative, fileName);
            }
            return fileNames;
        }

        // áp dụng cho upload city
        // mỗi city chỉ có 1 ảnh duy nhất nên không cần phân chia thư mục để chứa nhiều ảnh cho 1 city
        // add 1 ảnh vào 1 thư mục
        #region city
        public async Task<bool> AddCityImage(IFormFile file, string folder)
        {
            bool result = false;
            try
            {
                result = await SaveFile(file, folder);
                return result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void DeleteCityImage(string fileName)
        {
            string[] items = fileName.Split("/");
            var rootpath = hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(rootpath, "images");
            string filePath = Path.Combine(folderPath, items[1], items[2]);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<bool> UpdateCityImage(string oldImg, IFormFile file, string folder)
        {
            bool result = false;
            try
            {

                string[] items = oldImg.Split("/");
                string oldFilePath = Path.Combine(hostEnvironment.WebRootPath, "images");

                foreach (var f in items)
                {
                    oldFilePath = Path.Combine(oldFilePath, f);
                }

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string newFileName = file.FileName;
                string newFilePath = Path.Combine(hostEnvironment.WebRootPath, "images", folder, newFileName); ;


                // kiểm tra đường dẫn thư đã tồn tại chưa
                // nếu chưa thì tạo mới
                //if(System.IO.Directory.Exists(filePath) == false)
                //{
                //    System.IO.Directory.CreateDirectory(filePath);
                //}

                // nếu đã tồn tại đường dẫn file trước đó thì xóa file cũ
                if (System.IO.File.Exists(newFilePath) == true)
                {
                    System.IO.File.Delete(newFilePath);
                }

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
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

        #endregion

        #region hotel
        
        public void DeleteImgHotel(string url)
        {
            var filePath = GetPath(url);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void DeleteAllImgHotel(string hotelId)
        {
            var filePath = GetPath("hotels", hotelId, "_imgHotel");
            if (Directory.Exists(filePath))
            {
                DirectoryInfo di = new DirectoryInfo(filePath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        #endregion

        #region room

        public async Task<string> AddRoomImages(List<IFormFile> files, Hotel hotel, Room room)
        {
            try
            {
                string urlImgFolder = "";
                var folderRoom = GetPath("hotels", hotel.Id.ToString(), room.Id.ToString());
                bool result = false;
                if (Directory.Exists(folderRoom) == false)
                {
                    Directory.CreateDirectory(folderRoom);
                }

                // lưu ảnh hotel vào thư mục imgHotel
                foreach (var file in files)
                {
                    result = await SaveFile(file, folderRoom);
                    if (result == false)
                    {
                        break;
                    }
                }
                urlImgFolder = $"/hotels/{hotel.Id}/{room.Id}";
                return urlImgFolder;
            }
            catch
            {

            }
            return "";
        }

        public string GetFirstImageOfRoom(string photoPath)
        {
            try
            {
                if(string.IsNullOrEmpty(photoPath))
                {
                    return null;
                }
                var folder = GetPath(photoPath);
                if (Directory.Exists(folder) == false)
                {
                    return "";
                }
                var files = Directory.GetFiles(folder);
                if (files.Length == 0)
                {
                    return "";
                }
                return Path.GetFileName(files[0]);
            }
            catch { }
            return null;
        }

        public void DeleteRoomImage() { }

        public void DeleteAllRoomImage()
        {

        }
        #endregion


        public async Task<string> UploadImages(List<IFormFile> files, Hotel hotel, params Room[] room)
        {
            bool result = false;
            string urlImgFolder = "";
            try
            {
                // thư mục chứa ảnh hotel và rooms
                string folderOfHotel = GetPath("hotels", hotel.Id.ToString());

                string folderImgOfHotel = "";
                if (room.Length == 0)
                {
                    // thư mục chỉ chứa ảnh của hotel
                    folderImgOfHotel = GetPath(folderOfHotel, "_imgHotel");
                }
                else
                {
                    folderImgOfHotel = GetPath(folderOfHotel, room[0].Id.ToString());
                }
                
                // nếu chưa có thư mục chứa ảnh của hotel thì tạo mới
                if (System.IO.Directory.Exists(folderOfHotel) == false)
                {
                    System.IO.Directory.CreateDirectory(folderOfHotel);
                    System.IO.Directory.CreateDirectory(folderImgOfHotel);
                }

                // lưu ảnh hotel vào thư mục imgHotel
                foreach (var file in files)
                {
                    result = await SaveFile(file, folderImgOfHotel);
                    if (result == false)
                    {
                        break;
                    }
                }
                urlImgFolder = $"/hotels/{hotel.Id}/_imgHotel";
            }
            catch { }
            return urlImgFolder;
        }

        private string GetPath(params string[] folderOrFileName)
        {
            var imagesPath = Path.Combine(hostEnvironment.WebRootPath, "images");
            string filePath = imagesPath;
            foreach (var f in folderOrFileName)
            {
                if(f.StartsWith("/"))
                {
                    string[] temp = f.Split("/");
                    foreach(var _f in temp)
                    {
                        filePath = Path.Combine(filePath, _f);
                    }
                } else
                {
                    filePath = Path.Combine(filePath, f);
                }
            }
            return filePath;
        }

        private async Task<bool> SaveFile(IFormFile file, string folder)
        {
            bool result = false;
            string fileName = file.FileName;
            string filePath = GetPath(folder, fileName);

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

        // tour
        public async Task<string> UploadTourImages(List<IFormFile> files, Tour tour)
        {
            bool result = false;
            string urlImgFolder = "";
            try
            {
                string folderOfTour = GetPath("tours", tour.TourId.ToString());

                // nếu chưa có thư mục chứa ảnh của hotel thì tạo mới
                if (System.IO.Directory.Exists(folderOfTour) == false)
                {
                    System.IO.Directory.CreateDirectory(folderOfTour);
                }

                // lưu ảnh hotel vào thư mục imgHotel
                foreach (var file in files)
                {
                    result = await SaveFile(file, folderOfTour);
                    if (result == false)
                    {
                        break;
                    }
                }
                urlImgFolder = $"/tours/{tour.TourId}";
            }
            catch { }
            return urlImgFolder;
        }

        public bool DeleteAllImages(params string[] folder)
        {
            var filePath = GetPath(folder);
            if (Directory.Exists(filePath))
            {
                DirectoryInfo di = new DirectoryInfo(filePath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            return true;
        }


        public void DeleteImg(params string[] folder)
        {
            var filePath = GetPath(folder);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
