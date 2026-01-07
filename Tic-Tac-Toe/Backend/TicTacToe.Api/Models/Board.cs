using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Models
{
    public class Board
    {
        public PlayerSymbol[,] Cells { get; set; } = new PlayerSymbol[3,3];

        public bool IsFull() {
            for (int r=0;r<3;r++) for (int c=0;c<3;c++) if (Cells[r,c]==PlayerSymbol.None) return false;
            return true;
        }
    }
}