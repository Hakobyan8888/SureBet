using SureBet.Betting_Service;
using SureBet.Models;

namespace SureBet.ViewModels
{
    public class OddsViewModel
    {
        private OddsApiController _controller;

        public List<SureBetBase> H2HSureBets { get; set; }

        public List<TotalSureBet> TotalSureBets { get; set; }

        public OddsViewModel()
        {
            _controller = new OddsApiController();
            H2HSureBets = new List<SureBetBase>();
            TotalSureBets = new List<TotalSureBet>();
        }

        public async Task GetBestOdds()
        {
            var sports = await _controller.GetSportsAsync();

            foreach (var sport in sports)
            {
                var odds = await _controller.GetOddsAsync(sport.Key, "us,eu", "h2h,totals");
                if (odds == null) continue;
                foreach (var odd in odds)
                {
                    FilterBookmakers(odd.Bookmakers, odd);
                }
            }
        }

        public void FilterBookmakers(List<Bookmaker> bookmakers, GameRoot odd)
        {

            var homeTeam = odd.home_team;
            var awayTeam = odd.away_team;
            var startTime = odd.CommenceTime;
            var sportTitle = odd.SportTitle;

            Dictionary<string, Outcome> bestOutcomes = new Dictionary<string, Outcome>();

            foreach (var bookmaker in bookmakers)
            {
                var bookmakerTitle = bookmaker.Title;
                foreach (var market in bookmaker.Markets)
                {
                    if (market.Key == Helper.Helper.H2H_MARKET_NAME)
                    {
                        CheckH2H(market, bookmakerTitle, ref bestOutcomes);
                    }
                    else if (market.Key == Helper.Helper.TOTALS)
                    {
                        //CheckTotals(market);
                    }
                }
            }

            var isValid = CheckForSureBet(bestOutcomes.Values.ToList());
            if (isValid.Item1)
            {
                SureBetBase h2HSureBet = new SureBetBase()
                {
                    AwayTeam = awayTeam,
                    HomeTeam = homeTeam,
                    StartTime = startTime,
                    SportTitle = sportTitle,
                    StakeSizes = CalculateStakes(isValid.Item2, bestOutcomes.Values.ToList()),
                    ProfitPercent = isValid.Item2
                };
                H2HSureBets.Add(h2HSureBet);

            }
        }

        /// <summary>
        /// Check if there is H2H SureBet
        /// </summary>
        /// <param name="market">h2h market</param>
        /// <param name="odd">GameRoot</param>
        /// <param name="bookmakerTitle">the name of the bookmaker</param>
        /// <param name="homeWin">bookmaker name and Outcome</param>
        /// <param name="awayWin">bookmaker name and Outcome</param>
        /// <param name="draw">bookmaker name and Outcome</param>
        public void CheckH2H(Market market, string bookmakerTitle,
            ref Dictionary<string, Outcome> outcomesDict)
        {
            foreach (var outcome in market.Outcomes)
            {
                if (!outcomesDict.ContainsKey(outcome.Name))
                {
                    outcome.BookmakerTitle = bookmakerTitle;
                    outcomesDict.Add(outcome.Name, outcome);
                }
                else if (outcomesDict.ContainsKey(outcome.Name) && outcomesDict.TryGetValue(outcome.Name, out var value) && value.Price < outcome.Price)
                {
                    outcome.BookmakerTitle = bookmakerTitle;
                    outcomesDict[outcome.Name] = outcome;
                }
                //if (outcome.Name == homeTeam)
                //{
                //    if (homeWin == null)
                //    {
                //        homeWin = outcome;
                //        homeWin.BookmakerTitle = bookmakerTitle;
                //    }
                //    else if (homeWin.Price < outcome.Price)
                //    {
                //        homeWin = outcome;
                //        homeWin.BookmakerTitle = bookmakerTitle;
                //    }
                //}
                //else if (outcome.Name == awayTeam)
                //{
                //    if (awayWin == null)
                //    {
                //        awayWin = outcome;
                //        awayWin.BookmakerTitle = bookmakerTitle;
                //    }
                //    else if (awayWin.Price < outcome.Price)
                //    {
                //        awayWin = outcome;
                //        awayWin.BookmakerTitle = bookmakerTitle;
                //    }
                //}
                //else if (outcome.Name == Helper.Helper.DRAW)
                //{
                //    if (draw == null)
                //    {
                //        draw = outcome;
                //        draw.BookmakerTitle = bookmakerTitle;
                //    }
                //    else if (draw.Price < outcome.Price)
                //    {
                //        draw = outcome;
                //    }
                //}
            }

            //List<Outcome> outcomes = new List<Outcome>();
            //outcomes.Add(homeWin);
            //outcomes.Add(awayWin);
            //if (draw != null)
            //    outcomes.Add(draw);
        }

        public void CheckTotals(Market market)
        {
            Outcome over = null;
            Outcome under = null;

            foreach (var outcome in market.Outcomes)
            {
                if (outcome.Name == Helper.Helper.OVER)
                {
                    if (over == null)
                    {
                        over = outcome;
                    }
                    else if (over.Price < outcome.Price && outcome.Point == over.Point)
                    {
                        over = outcome;
                    }
                }
                else if (outcome.Name == Helper.Helper.UNDER)
                {
                    if (under == null)
                    {
                        under = outcome;
                    }
                    else if (under.Price < outcome.Price && outcome.Point == under.Point)
                    {
                        under = outcome;
                    }
                }
            }

            List<Outcome> outcomes = new List<Outcome>();
            outcomes.Add(over);
            outcomes.Add(under);
            var isValid = CheckForSureBet(outcomes);
            if (isValid.Item1)
            {

            }
        }

        /// <summary>
        /// Check if surebet exists
        /// </summary>
        /// <param name="outcomes">bet name(team name) and Outcome</param>
        /// <returns>Tuple<bool, double> 1.if it is surebet, 2. percent of winning</returns>
        public Tuple<bool, double> CheckForSureBet(List<Outcome> outcomes)
        {
            double percent = 0;
            foreach (var outcome in outcomes)
            {
                percent = percent + (1 / outcome.Price);
            }
            if (percent < 1)
            {
                return Tuple.Create(true, percent);
            }
            else
            {
                return Tuple.Create(false, percent);
            }
        }

        /// <summary>
        /// Calculate the amount of stakes for each odd
        /// </summary>
        /// <param name="percent">the percent of winning chance</param>
        /// <param name="outcomes">the outcomes of the game</param>
        /// <returns></returns>
        public List<Stake> CalculateStakes(double percent, List<Outcome> outcomes)
        {
            List<Stake> stakes = new List<Stake>();
            foreach (var outcome in outcomes)
            {
                var stakeSize = 100 / outcome.Price / percent;
                stakes.Add(new Stake
                {
                    BetAmount = stakeSize,
                    BetName = outcome.Name,
                    BookmakerName = outcome.BookmakerTitle,
                    Point = outcome.Point
                });
            }
            return stakes;
        }
    }
}
