using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourWayChess
{
    public class GameUtils
    {
        public static bool IsInsideBoard(int x, int y)
        {
            return (!(x < 3 && y < 3) &&
                    !(x > 10 && y < 3) && 
                    !(x < 3 && y > 10) && 
                    !(x > 10 && y > 10) &&
                    x>0 && y >0 && 
                    x< GameDispatcher.GameBoard.GetLength(0) && 
                    y<GameDispatcher.GameBoard.GetLength(1));
        }

        public static bool ContainsPiece(int x, int y)
        {
            return GameDispatcher.GameBoard[x, y] != null;
        }


    }
}
