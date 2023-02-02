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

        //Gets core rules reference for printing an army list
        private string RulesFileName()
        {
            return Path.Combine(WebHostEnvironment.WebRootPath, "data", "core", "Core_Rules_Reference.json");
        }

        //Gets army specific rules reference for printing an army list
        private string RulesFileName(string name)
        {
            return Path.Combine(WebHostEnvironment.WebRootPath, "data", "armies", name, "Army_Rules_Reference.json");
        }

        private string ArmyListFileName(string name)
        {
            return Path.Combine(WebHostEnvironment.WebRootPath, "data", "armies", name, "Unit_List.json");
        }

        public IEnumerable<Unit> GetUnits(string name)
        {
            
            try
            {
                using (var jsonFileReader = File.OpenText(ArmyListFileName(name)))
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

        public Rules getArmyRules()
        {
            try
            {
                using (var jsonFileReader = File.OpenText(RulesFileName()))
                {
                    return JsonConvert.DeserializeObject<Rules>(jsonFileReader.ReadToEnd());
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }

        public List<Rules> getArmyRules(string name)
        {
            try
            {
                using (var jsonFileReader = File.OpenText(RulesFileName(name)))
                {                   
                    return JsonConvert.DeserializeObject<List<Rules>>(jsonFileReader.ReadToEnd());
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
