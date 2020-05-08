using jogo_xadres_console.Tabuleiro;
using System;

namespace jogo_xadres_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard          chessBoard = new ChessBoard(8,8);

            ChessScreen.PrintChessBoard(chessBoard);
        }
    }
}
