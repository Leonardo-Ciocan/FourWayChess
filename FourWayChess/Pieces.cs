using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FourWayChess
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Bishop,
        King,
        Queen,
        Knight
    }

    public class Piece
    {
        public Point Position;
        public PieceType Type;

        public Piece(PieceType pt)
        {
            Type = pt;
        }
    }
}
