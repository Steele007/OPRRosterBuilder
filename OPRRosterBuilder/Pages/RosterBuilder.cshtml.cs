using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OPRRosterBuilder.Models;
using OPRRosterBuilder.Services;
using System.Web;

namespace OPRRosterBuilder.Pages
{
    public class RosterBuilderModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileReaderService jsonService;
        public IEnumerable<Unit> Units { get; private set; }
        public List<Unit> ArmyList { get; set; }
        public int Points { get; set; }
        public RosterBuilderModel(ILogger<IndexModel> logger, JsonFileReaderService jsonService)
        {
            _logger = logger;
            this.jsonService = jsonService;
        }
        public void OnGet()
        {
            string armyName = Request.Query["p"];
            Units = jsonService.GetUnits(armyName);
            Points = 0;
            ArmyList = new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            Points += unit.Points;
            ArmyList.Add(unit);
        }
        
    }
}
