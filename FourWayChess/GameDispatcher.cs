using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourWayChess
{
    public static class GameDispatcher
    {
        public static object[,] GameBoard = new object[14, 14]
        {
            {null     ,null   ,null   ,rook()   ,knight() ,bishop() ,queen() ,king()  ,bishop() ,knight() ,rook()  ,null  ,null   ,null},
            {null     ,null   ,null   ,pawn()   ,pawn()   ,pawn()   ,pawn()  ,pawn()  ,pawn()   ,pawn()   ,pawn()  ,null  ,null   ,null},
            {null     ,null   ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,null   ,null},
            {rook()   ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,rook()},
            {knight() ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,knight()},
            {bishop() ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,bishop()},
            {queen()  ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,king()},
            {king()   ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,queen() },
            {bishop() ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,bishop()},
            {knight() ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,knight()},
            {rook()   ,pawn() ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,pawn() ,rook()},
            {null     ,null   ,null   ,null     ,null     ,null     ,null    ,null     ,null     ,null    ,null    ,null  ,null   ,null},
            {null     ,null   ,null   ,pawn()   ,pawn()   ,pawn()   ,pawn()  ,pawn()  ,pawn()   ,pawn()   ,pawn()  ,null  ,null   ,null},
            {null     ,null   ,null   ,rook()   ,knight() ,bishop() ,king()  ,queen() ,bishop() ,knight() ,rook()  ,null  ,null   ,null}

        };


        public static Piece pawn()
        {
            return new Piece(PieceType.Pawn);
        }

        public static Piece bishop()
        {
            return new Piece(PieceType.Bishop);
        }

        public static Piece rook()
        {
            return new Piece(PieceType.Rook);
        }

        public static Piece knight()
        {
            return new Piece(PieceType.Knight);
        }

        public static Piece king()
        {
            return new Piece(PieceType.King);
        }

        public static Piece queen()
        {
            return new Piece(PieceType.Queen);
        }
    }
}
