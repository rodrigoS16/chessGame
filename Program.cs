using Chess;
using jogo_xadres_console.Tabuleiro;
using System;

namespace jogo_xadres_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard          chessBoard = new ChessBoard(8,8);

            chessBoard.PutNewPiece(new TowerPiece(chessBoard,Tabuleiro.Enums.Color.Black), new Position(0, 0));
            chessBoard.PutNewPiece(new TowerPiece(chessBoard, Tabuleiro.Enums.Color.Black), new Position(1, 3));
            chessBoard.PutNewPiece(new KingPiece(chessBoard, Tabuleiro.Enums.Color.Black), new Position(2, 4));

            ChessScreen.PrintChessBoard(chessBoard);
        }
    }
}
