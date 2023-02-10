using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class TotalSureBet
    {
        public string BookmakerTitle { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string StartTime { get; set; }
        public Outcome Over { get; set; }
        public Outcome Under { get; set; }
    }
}
