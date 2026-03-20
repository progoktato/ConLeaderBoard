using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLeaderBoard.Models
{
    public class Result
    {
        string name;
        DateTime date;
        int point;
        //TimeOnly futasiIdo;

        public Result(string name, int point)
        {
            this.name = name;
            this.point = point;
            this.date = DateTime.Now;
        }

        public string Name { get => name; }
        public DateTime Date { get => date; }
        public int Point { get => point; }

        public string GetString()
        {
            return $"{name} - {point} points - {date.ToShortDateString()}";
        }
    }
}
