using ConLeaderBoard.Models;

namespace ConLeaderBoard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LeaderBoard flipper = new LeaderBoard(12);

            flipper.AddResult("Kiss Béla", 145);
            flipper.AddResult("Mákos Álmos", 167);
            flipper.AddResult("Tépett Béla", 34);

            flipper.AddResult("Szabó Éva", 145);
            flipper.AddResult("Best Ádám", 210);
            //flipper.AddResult("Tépett Béla", 134);
            flipper.AddResult("Marok Csilla", 145);
            flipper.AddResult("Nagy Márta", 210);
            flipper.AddResult("Sas Péter", 98);

            flipper.AddResult("1", 105);
            flipper.AddResult("2", 105);
            flipper.AddResult("4", 105);
            flipper.AddResult("5", 105);

            foreach (var item in flipper.GetBoard())
            {
                Console.WriteLine(item);
            }

            flipper.RemoveResult("Szabó Éva");
            flipper.RemoveResult("Mákos Álmos");
            foreach (var item in flipper.GetBoard())
            {
                Console.WriteLine(item);
            }

        }

    }
}
