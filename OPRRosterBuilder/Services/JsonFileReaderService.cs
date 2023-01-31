using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OPRRosterBuilder.Models;
using Newtonsoft.Json;

namespace OPRRosterBuilder.Services
{
    public class JsonFileReaderService
    {
        public JsonFileReaderService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName()
        {
             return Path.Combine(WebHostEnvironment.WebRootPath, "data", "armies", "Battle Brothers", "Unit_List.json");
        }

        private string JsonFileName(string name)
        {
            return Path.Combine(WebHostEnvironment.WebRootPath, "data", "armies", name, "Unit_List.json");
        }


        public IEnumerable<Unit> GetUnits()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName()))
            {
                /*return JsonSerializer.Deserialize<Unit[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                {
                    IncludeFields = true,
                });*/
                return JsonConvert.DeserializeObject<Unit[]>(jsonFileReader.ReadToEnd());
            }
        }

        public IEnumerable<Unit> GetUnits(string name)
        {
            
            try
            {
                using (var jsonFileReader = File.OpenText(JsonFileName(name)))
                {
                    /*return JsonSerializer.Deserialize<Unit[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    {
                        IncludeFields = true,
                    });*/
                    return JsonConvert.DeserializeObject<Unit[]>(jsonFileReader.ReadToEnd());
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }
    }

}
