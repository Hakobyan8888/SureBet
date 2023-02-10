
using SureBet.ViewModels;

OddsViewModel oddsViewModel = new OddsViewModel();

await oddsViewModel.GetBestOdds();

foreach(var h2hSureBet in oddsViewModel.H2HSureBets)
{
    Console.WriteLine(h2hSureBet.ProfitPercent);
}
//foreach(var h2hSureBet in oddsViewModel.TotalSureBets)
//{
//}


Console.ReadLine();