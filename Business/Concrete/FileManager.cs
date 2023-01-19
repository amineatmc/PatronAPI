using Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf("."));
            fileFormat = fileFormat.ToLower();
            string fileName=Guid.NewGuid().ToString() + fileFormat;
            string path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public void FileDeleteToServer(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private IResult CheckIfImageExtesionsAllow(string fileName)
        //{
        //    var ext = fileName.Substring(fileName.LastIndexOf('.'));
        //    var extension = ext.ToLower();
        //    List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
        //    if (!AllowFileExtensions.Contains(extension))
        //    {
        //        return new ErrorResult("Eklediğiniz resim .jpg, .jpeg, .gif, .png türlerinden biri olmalıdır!");
        //    }
        //    return new SuccessResult();
        //}

        //private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        //{
        //    decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
        //    if (imgMbSize > 1)
        //    {
        //        return new ErrorResult("Yüklediğiniz resmi boyutu en fazla 1mb olmalıdır");
        //    }
        //    return new SuccessResult();
        //}

    }
}
