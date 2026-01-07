using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Api.Game.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PlayerXId { get; set; }
        public Guid PlayerOId { get; set; }
        public PlayerSymbol[,] Board { get; set; } = new PlayerSymbol[3,3];
        public PlayerSymbol Turn { get; set; } = PlayerSymbol.X;
        public bool IsFinished { get; set; } = false;
        public PlayerSymbol Winner { get; set; } = PlayerSymbol.None;

        public bool MakeMove(int row, int col, PlayerSymbol symbol)
        {
            if (IsFinished) return false;
            if (Board[row,col] != PlayerSymbol.None) return false;
            Board[row,col] = symbol;
            CheckWinner();
            Turn = Turn == PlayerSymbol.X ? PlayerSymbol.O : PlayerSymbol.X;
            return true;
        }

        private void CheckWinner()
        {
            PlayerSymbol[,] b = Board;
            PlayerSymbol CheckLine((int r,int c) a, (int r,int c) b2, (int r,int c) c2)
            {
                if (Board[a.r,a.c] != PlayerSymbol.None && Board[a.r,a.c] == Board[b2.r,b2.c] && Board[a.r,a.c] == Board[c2.r,c2.c])
                    return Board[a.r,a.c];
                return PlayerSymbol.None;
            }

            var lines = new[] {
                ( (0,0),(0,1),(0,2) ), ((1,0),(1,1),(1,2)), ((2,0),(2,1),(2,2)),
                ((0,0),(1,0),(2,0)), ((0,1),(1,1),(2,1)), ((0,2),(1,2),(2,2)),
                ((0,0),(1,1),(2,2)), ((0,2),(1,1),(2,0))
            };

            foreach(var (a,b2,c2) in lines)
            {
                var w = CheckLine(a,b2,c2);
                if (w != PlayerSymbol.None)
                {
                    Winner = w; IsFinished = true; return;
                }
            }

            // check draw
            bool full = true;
            for (int r=0;r<3;r++) for (int c=0;c<3;c++) if (Board[r,c]==PlayerSymbol.None) full = false;
            if (full) { IsFinished = true; Winner = PlayerSymbol.None; }
        }
    }
}