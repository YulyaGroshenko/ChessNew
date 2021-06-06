using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChessReserve.Logic
{
    public class Player
    {
        public string Name { get; private set; }
        public int Victorys { get; set; }
        public Figure[,] Field { get; set; }
        public Sides Side { get; set; }
        public Player(string name)
        {
            Name = name;
            Side = Sides.White;
        }
        //public int AddVictorys()
        //{
        //    if (File.Exists($"{FileWorker.Saves}\\{Name}"))
        //    {
        //        Player player = FileWorker.GetPlayer(Name);
        //        return player.Victorys++;
        //    }
        //    else
        //    {
        //        Victorys += 1;
        //    }
        //    return Victorys;
        //}
    }
}
