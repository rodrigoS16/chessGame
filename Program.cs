using Chess;
using jogo_xadres_console.Tabuleiro;
using System;
using Tabuleiro.Exception;

namespace jogo_xadres_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard          chessBoard = new ChessBoard(8,8);

            try
            {
                chessBoard.PutNewPiece(new TowerPiece(chessBoard, Tabuleiro.Enums.Color.Black), new Position(0, 0));
                chessBoard.PutNewPiece(new TowerPiece(chessBoard, Tabuleiro.Enums.Color.Black), new Position(1, 3));
                chessBoard.PutNewPiece(new KingPiece(chessBoard, Tabuleiro.Enums.Color.Black), new Position(0, 2));
                

                ChessScreen.PrintChessBoard(chessBoard);

                Console.WriteLine();
                ChessPosition pos = new ChessPosition('c', 7);
                Console.WriteLine(pos);
                Console.WriteLine(pos.ToPosition());
            }
            catch (ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
