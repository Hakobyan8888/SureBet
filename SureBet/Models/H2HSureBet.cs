using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class H2HSureBet : SureBetBase
    {
        public Tuple<string, Outcome> HomeWin { get; set; }
        public Tuple<string, Outcome> AwayWin { get; set; }
        public Tuple<string, Outcome> Draw { get; set; }
    }
}
