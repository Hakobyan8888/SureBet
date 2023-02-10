using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class SureBetBase
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double ProfitPercent { get; set; }
        public string SportTitle { get; set; }
        public DateTime StartTime { get; set; }
        public List<Stake> StakeSizes { get; set; }
    }
}
