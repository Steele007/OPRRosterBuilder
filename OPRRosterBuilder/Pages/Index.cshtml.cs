using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OPRRosterBuilder.Models;
using OPRRosterBuilder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPRRosterBuilder.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileReaderService jsonService;  
        public IEnumerable<Unit> Units { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileReaderService jsonService)
        {
            _logger = logger;
            this.jsonService = jsonService;
            
        }

        public void OnGet()
        {
            Units = jsonService.GetUnits();
        }

        public IEnumerable<Unit> getArmyList(string listName)
        {
            return null;
        }
    }
}
