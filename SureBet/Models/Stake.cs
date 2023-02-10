using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureBet.Models
{
    public class Stake
    {
        /// <summary>
        /// The amount to bet
        /// </summary>
        public double BetAmount { get; set; }

        /// <summary>
        /// The team name to bet on
        /// </summary>
        public string BetName { get; set; }

        /// <summary>
        /// The bookmaker name in wich to bet
        /// </summary>
        public string BookmakerName { get; set; }

        /// <summary>
        /// The point of the odd(over/under 2.5)
        /// </summary>
        public double Point { get; set; }
    }
}
