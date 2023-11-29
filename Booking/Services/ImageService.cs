
using Booking.Interfaces;
using Booking.Models;

namespace Booking.Services
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
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
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
            catch
            {
                return false;
            }
        }

        public void DeleteCityImage(string fileName)
        {
            if (fileName != "/no-image.jpg")
            {
                return;
            }
            string[] items = fileName.Split("/");
            var rootpath = hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(rootpath, "resources", "images");
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
                string oldFilePath = Path.Combine(hostEnvironment.WebRootPath, "resources", "images");

                foreach (var f in items)
                {
                    oldFilePath = Path.Combine(oldFilePath, f);
                }

                if (File.Exists(oldFilePath) && oldImg != "/no-image.jpg")
                {
                    File.Delete(oldFilePath);
                }

                string newFileName = file.FileName;
                string newFilePath = Path.Combine(hostEnvironment.WebRootPath, "resources", "images", folder, newFileName); ;


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
            catch
            {

            }
            return result;
        }

        #endregion


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




		public async Task<string> UploadImages(List<IFormFile> files, params string[] folder)
		{
			bool result = false;
			string urlImgFolder = "";
			try
			{
				// thư mục chứa ảnh hotel và rooms
				string imageFolderOfHotel = GetPath(folder);

				// nếu chưa có thư mục chứa ảnh của hotel thì tạo mới
				if (System.IO.Directory.Exists(imageFolderOfHotel) == false)
				{
					//System.IO.Directory.CreateDirectory(imageFolderOfHotel);

					// Tách đường dẫn thành các thư mục con
					string[] folders = imageFolderOfHotel.Split('\\');

					// Bắt đầu từ thư mục gốc (index 0)
					string currentPath = folders[0];

					// Tạo các thư mục con theo đường dẫn nhập
					for (int i = 1; i < folders.Length; i++)
					{
						currentPath = Path.Combine(currentPath, folders[i]);

						// Kiểm tra xem thư mục đã tồn tại chưa
						if (!Directory.Exists(currentPath))
						{
							// Nếu chưa tồn tại, tạo mới
							Directory.CreateDirectory(currentPath);
						}
					}
				}
				else
				{
					string[] olgLmg = Directory.GetFiles(imageFolderOfHotel);
					// Xóa từng tệp tin một
					foreach (string file in olgLmg)
					{
						File.Delete(file);
					}
				}

				// lưu ảnh hotel vào thư mục imgHotel
				foreach (var file in files)
				{
					result = await SaveFile(file, imageFolderOfHotel);
					if (result == false)
					{
						break;
					}
				}
				urlImgFolder = "/" + string.Join(@"/", folder);
			}
			catch { }
			return urlImgFolder;
		}

		private string GetPath(params string[] folderOrFileName)
		{
			var imagesPath = Path.Combine(hostEnvironment.WebRootPath, "resources", "images");
			string filePath = imagesPath;
			foreach (var f in folderOrFileName)
			{
				if (f.StartsWith("/"))
				{
					string[] temp = f.Split("/");
					foreach (var _f in temp)
					{
						filePath = Path.Combine(filePath, _f);
					}
				}
				else
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
