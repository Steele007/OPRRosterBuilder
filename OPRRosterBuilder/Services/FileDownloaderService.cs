using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPRRosterBuilder.Services
{
    public class FileDownloaderService
    {
        public FileDownloaderService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public void downloadFile(string content, string name, string fileType)
        {
            //return File(content, fileType, name);
        }
    }
}
