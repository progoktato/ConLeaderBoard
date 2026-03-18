using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLeaderBoard.Models
{
    public class LeaderBoard
    {
        List<Result> results;
        int maxSize;

        /// <summary>
        /// Ponttáblázat létrehozása a megadott mérettel
        /// </summary>
        /// <param name="size"></param>
        public LeaderBoard(byte size)
        {
            results = new List<Result>();
            this.maxSize = size;
        }


        /// <summary>
        /// A ponttáblázat maximális mérete
        /// </summary>
        public int MaxSize { get => maxSize; }

        /// <summary>
        /// A játékos eredményének rögzítése
        /// </summary>
        /// <param name="name">jatékos neve</param>
        /// <param name="point">a játékban elért pontszáma</param>
        public void AddResult(string name, int point)
        {
            // Ha már szerepelt, akkor hol van?
            int existNameIndex = -1;  //Nincs ilyen név még!
            int index = 0;
            for (index = 0; index < results.Count; index++)
            {
                if (this.results[index].Name == name)
                {
                    existNameIndex = index;
                    break;
                }
            }

            if (existNameIndex >= 0)  //Találtam már ilyen névvel
            {
                //Jobb eredménye van
                if (this.results[existNameIndex].Point < point)
                {
                    //this.results[existNameIndex].Point= point;   //Rossz, mivel nincs hozzáférés

                    this.results[existNameIndex] = new Result(name, point);
                }
                return;
            }

            // ha új név van
            index = 0;
            while (index < results.Count && point < results[index].Point)
            {
                index++;
            }
            results.Insert(index, new Result(name, point));

            // Ha már tele a lista
            if (results.Count > this.maxSize)
            {
                results.RemoveAt(results.Count - 1);
            }
        }

        /// <summary>
        /// Alaphelyzetbe állítja az eredményjelzőt
        /// </summary>
        public void Reset()
        {
            results = new List<Result>();
        }

        /// <summary>
        /// Az eredményjelző aktuális állapotát adja vissza egy formázott string listában, ahol minden string egy sort jelent a táblázatban. Az első sor a fejléc, a többi sor pedig a játékosok eredményeit tartalmazza. A táblázat rendezett a pontszámok szerint csökkenő sorrendben, és azonos pontszám esetén azonos helyezést kapnak a játékosok. A helyezés számozása 1-től kezdődik, és a helyezés után megjelenik a játékos neve, pontszáma és a dátum, amikor elérték ezt az eredményt.
        /// </summary>
        /// <returns>Eredmény lista formátumban</returns>
        public List<String> GetBoard()
        {
            List<String> board = new List<String>();
            board.Add(GetHeader("Sorszám", "Játékos", "Pontszám", "Dátum"));
            int position = 0;
            int counter = 0;
            Result previous = null;
            foreach (Result result in results)
            {
                position++;
                if (previous != null && result.Point == previous.Point)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                board.Add($"│{(position - counter).ToString().PadLeft(7)}│{result.Name.PadRight(30, '.')}│{result.Point.ToString().PadLeft(12)} p│{result.Date.ToShortDateString().PadLeft(20)}│");
                previous = result;
            }
            return board;
        }


        private string GetHeader(string s0, string s1, string s2, string s3)
        {
            return $"│{s0.PadRight(7)}│{s1.PadRight(30)}│{s2.PadLeft(14)}│{s3.PadRight(20)}│";
        }

        /// <summary>
        /// A megadott helyezéshez tartozó eredményeket adja vissza egy listában. Ha a helyezés nem létezik, akkor üres listát ad vissza. Az eredmények rendezettek a pontszámok szerint csökkenő sorrendben, és azonos pontszám esetén azonos helyezést kapnak a játékosok. A helyezés számozása 1-től kezdődik.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public List<Result> GetResults(int position)
        {
            Dictionary<int, List<Result>> positions = new();

            // TODO itt jönne a fincsi fáncsi

            return positions[position];
        }
    }
}
