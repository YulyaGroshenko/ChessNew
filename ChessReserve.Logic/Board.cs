using System;

namespace ChessReserve.Logic
{
    public class Board
    {
        public Figure[,] Field { get; private set; }

        public Board()
        {
            Field = new Figure[8, 8]
            {
                { new Rook(Sides.Black), new Knight(Sides.Black), new Bishop(Sides.Black), new King(Sides.Black), new Queen(Sides.Black), new Bishop(Sides.Black), new Knight(Sides.Black), new Rook(Sides.Black) },
                { new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black), new Pawn(Sides.Black) },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White), new Pawn(Sides.White) },
                { new Rook(Sides.White), new Knight(Sides.White), new Bishop(Sides.White), new King(Sides.White), new Queen(Sides.White), new Bishop(Sides.White), new Knight(Sides.White), new Rook(Sides.White) },
            };
        }
        public void GetSaveField(Player player)
        {
            Field = player.Field;
        }
    }
}
