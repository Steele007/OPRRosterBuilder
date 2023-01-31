using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Controllers
{
    
    public class RosterBuilderController : Controller
    {
        public FileResult DownloadJson(string content, string name)
        {
            byte[] downloadFileContent = Encoding.ASCII.GetBytes(content);
            return File(downloadFileContent, System.Net.Mime.MediaTypeNames.Application.Json, name);
        }
    }
}
