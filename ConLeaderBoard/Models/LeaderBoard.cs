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

        //Dictionary<int,List<Result>> results;  //Érdemes lenne erre cserélni!

        Result[] tomb;
        int maxSize;

        /// <summary>
        /// A ranglista konstruktora, amely létrehozza a ranglistát egy üres listával és beállítja a maximális méretét a megadott érték alapján.
        /// </summary>
        /// <param name="size">A ranglista maximális mérete</param>
        public LeaderBoard(byte size)
        {
            results = new List<Result>();
            this.maxSize = size;
        }


        /// <summary>
        /// A ranglista maximális mérete
        /// </summary>
        public int MaxSize { get => maxSize; }

        /// <summary>
        /// A játékos eredményének rögzítése
        /// </summary>
        /// <param name="name">jatékos neve, ami egyedi azonosító</param>
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

            if (existNameIndex >= 0)  //Találtam már ilyen névvel eredményt
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
        /// A ranglista eredményeinek törlése, visszaállítva a ranglistát egy üres állapotba. Ez a művelet eltávolítja az összes korábbi eredményt, és lehetővé teszi a ranglista újraindítását.
        /// </summary>
        public void Reset()
        {
            results = new List<Result>();
        }

        /// <summary>
        /// A ranglista jelenlegi állapotának lekérdezése egy listában, ahol minden elem egy formázott string, amely tartalmazza a helyezést, a játékos nevét, a pontszámot és a dátumot. A helyezések számozása 1-től kezdődik, és azonos pontszám esetén azonos helyezést kapnak a játékosok. A lista első eleme egy fejléc, amely megnevezi az oszlopokat.
        /// </summary>
        /// <returns>Eredmény lista formátumban</returns>
        public List<String> GetBoard()
        {
            List<String> board = new List<String>();
            board.Add("┌───────┬──────────────────────────────┬──────────────┬────────────────────┐");
            board.Add($"│{"Sorszám".PadRight(7)}│{"Játékos".PadRight(30)}│{"Pontszám".PadLeft(14)}│{"Dátum".PadRight(20)}│");
            int position = 0;
            int counter = 0;
            Result previous = null;
            foreach (Result result in results)
            {
                board.Add("├───────┼──────────────────────────────┼──────────────┼────────────────────┤");
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
            board.Add("└───────┴──────────────────────────────┴──────────────┴────────────────────┘");
            return board;
        }

        public void RemoveResult(string name)
        {
            int index = 0;
            while (index < results.Count && results[index].Name != name)
            {
                index++;
            }

            //if (index == results.Count)
            //{
            //    return;
            //}
            //results.RemoveAt(index);

            if (index < results.Count)
            {
                results.RemoveAt(index);
            }

        }

        /// <summary>
        /// A ranglista adott helyezésének lekérdezése. A helyezés 1-től kezdődik, és azonos pontszám esetén azonos helyezést kapnak a játékosok. Ha a megadott helyezés érvénytelen (például kisebb, mint 1 vagy nagyobb, mint a ranglista mérete, vagy nincs ilyen helyezés), akkor egy üres listát ad vissza. 
        /// </summary>
        /// <param name="position">1-től kezdett számozás esetén a pozíció</param>
        /// <returns>Az eredmények listája, ami üres lista, ha nincs ilyen pozíció</returns>
        public List<Result> GetResults(int position)
        {
            List<Result> result = new List<Result>();
            if (position < 1 || position > results.Count)
            {
                return result;
            }
            int counter = 0;
            Result previous = null;
            foreach (Result res in results)
            {
                if (previous != null && res.Point == previous.Point)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                if (position - counter == position)
                {
                    result.Add(res);
                }
                previous = res;
            }
            return result;
        }
    }
}
