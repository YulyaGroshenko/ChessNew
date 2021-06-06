using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChessReserve.Logic
{
    public class Game
    {
        public Board Board { get; private set; } = new Board();
        public Player Player { get; private set; }
        public Game(string name)
        {
            if (File.Exists($"{FileWorker.Saves}\\{name}"))
            {
                Player = FileWorker.GetPlayer(name);
                Board.GetSaveField(Player);
            }
            else
            {
                Player = new Player(name);
            }
        }
        public void Play(Figure figure, int finishD, Letters finishL)
        {
            if (IsDirectionRight(figure, finishD) && figure.Side == Player.Side && CanMove(figure, finishD, finishL))
            {
                Board.Field[finishD, (int)finishL] = figure;
                Board.Field[figure.Digit, (int)figure.Letter] = null;
                figure.MoveFigure(finishD, finishL);
                ChangeSide();
            }
            else
            {
                throw new Exception("ты не можешь так сходить");
            }
        }
        private bool CanMove(Figure figure, int finishD, Letters finishL)
        {
            if ((IsCellEmpty(figure, finishD, finishL) ||
                Board.Field[finishD, (int)finishL].Side != Board.Field[figure.Digit, (int)figure.Letter].Side) && figure.CheckRightMove(finishD, finishL))
                return true;
            else
                return false;
        }
        private bool IsCellEmpty(Figure figure, int digit, Letters letter)
        {
            if (Board.Field[digit, (int)letter] == null)
                return true;
            else
                return false;
        }
        private bool IsDirectionRight(Figure figure, int finishD)
        {
            if (figure is Pawn)
            {
                if (figure.Side == Sides.Black && finishD > figure.Digit || figure.Side == Sides.White && finishD < figure.Digit)
                    return true;
                else
                    return false;
            }
            return true;
        }
        private void ChangeSide()
        {
            if (Player.Side == Sides.White)
                Player.Side = Sides.Black;
            else
                Player.Side = Sides.White;
        }
        public bool CheckEndGame()
        {
            bool figureHave = false;
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (Board.Field[i, k] == null)
                        continue;
                    else if (Board.Field[i, k].Side == Player.Side)
                        figureHave = true;
                }
            }
            return figureHave;
        }
        public void SaveGame(bool end)
        {
            if (end)
            {
                Player.Victorys += 1;
                Player.Field = (new Board()).Field;
                Player.Side = Sides.White;
            }
            else
            {
                Player.Field = Board.Field;
            }
            FileWorker.SavePlayer(Player);
            FileWorker.SetRecords(Player);
        }
        public void Cheats()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (Board.Field[i, k] == null)
                        continue;
                    else if (Board.Field[i, k].Side == Sides.Black)
                        Board.Field[i, k] = null;
                }
            }
        }
    }
}
