using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Sevices
{
    public class AttachmentServices : IAtttachmentServices
    {
        //Allowed Extension {.png, .jpg, jpeg}
        public readonly List<string> _allowedExtensions = new(){".png", ".jpg", ".jpeg"};
        //MAX SIZE => 2MB => 2,097,152 BYTES(2*1024*1024)

        public const int _allowedMaxsize = 2_097_152;
        public string? Upload(IFormFile file, string folderName)
        {
           //validate the file extension 
           var extinsion = Path.GetExtension(file.FileName);
            if(!_allowedExtensions.Contains(extinsion)) return null;

            //validate the size 

            if(file.Length>_allowedMaxsize) return null;

            // get located folder path 
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            //must image name be unique 

            var fileName = $"{Guid.NewGuid()}{extinsion}";

            // get the path of the file --> {folderPath + file name }

            var filePath = Path.Combine(folderPath, fileName);

            // save file as stream [data per time]

            var fileStream = new FileStream(filePath, FileMode.Create);

            //cop efile to file stream 

            file.CopyTo(fileStream);
            return fileName;



        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

       
    }
}
