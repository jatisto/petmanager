using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace pet_manager.Infrastructure
{
    public class FileUploader
    {
        public string Upload(IFormFile file){
            var dateTime = DateTime.Now.ToLongTimeString();


            var path = Path.Combine(Directory.GetCurrentDirectory(),
                                "wwwroot","images",dateTime+file.FileName);

            using(var stream = new FileStream(path,FileMode.Create)){
                    file.CopyTo(stream);
            }

            string fileName = dateTime+file.FileName;
            return fileName;
        }
    }
}