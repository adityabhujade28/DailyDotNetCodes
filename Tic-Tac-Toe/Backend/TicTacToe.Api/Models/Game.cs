using System;
using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PlayerXId { get; set; }
        public Guid PlayerOId { get; set; }
        public Board Board { get; set; } = new Board();
        public PlayerSymbol Turn { get; set; } = PlayerSymbol.X;
        public bool IsFinished { get; set; } = false;
        public PlayerSymbol Winner { get; set; } = PlayerSymbol.None;

        public bool MakeMove(Guid playerId, int row, int col)
        {
            PlayerSymbol symbol = PlayerSymbol.None;

            // Local mode: both players use the same browser (PlayerXId == PlayerOId or PlayerOId == Guid.Empty)
            bool localMode = (PlayerXId == PlayerOId) || (PlayerOId == Guid.Empty);
            if (localMode)
            {
                // symbol is determined by whose turn it is
                symbol = Turn;
            }
            else
            {
                if (playerId == PlayerXId) symbol = PlayerSymbol.X;
                else if (playerId == PlayerOId) symbol = PlayerSymbol.O;
                else return false;

                // ensure player moves only on their turn
                if (symbol != Turn) return false;
            }

            if (row < 0 || row > 2 || col < 0 || col > 2) return false;
            if (Board.Cells[row,col] != PlayerSymbol.None) return false;
            if (IsFinished) return false;

            Board.Cells[row,col] = symbol;
            CheckWinner();
            Turn = Turn == PlayerSymbol.X ? PlayerSymbol.O : PlayerSymbol.X;
            return true;
        }

        private void CheckWinner()
        {
            var b = Board.Cells;
            PlayerSymbol Check((int r,int c) a, (int r,int c) b2, (int r,int c) c2)
            {
                if (b[a.r,a.c] != PlayerSymbol.None && b[a.r,a.c]==b[b2.r,b2.c] && b[a.r,a.c]==b[c2.r,c2.c]) return b[a.r,a.c];
                return PlayerSymbol.None;
            }

            (int,int)[][] lines = new[] {
                new[]{(0,0),(0,1),(0,2)}, new[]{(1,0),(1,1),(1,2)}, new[]{(2,0),(2,1),(2,2)},
                new[]{(0,0),(1,0),(2,0)}, new[]{(0,1),(1,1),(2,1)}, new[]{(0,2),(1,2),(2,2)},
                new[]{(0,0),(1,1),(2,2)}, new[]{(0,2),(1,1),(2,0)}
            };

            foreach(var line in lines){
                var w = Check(line[0], line[1], line[2]);
                if (w!=PlayerSymbol.None) { Winner = w; IsFinished = true; return; }
            }

            if (Board.IsFull()) { IsFinished = true; Winner = PlayerSymbol.None; }
        }
    }
}