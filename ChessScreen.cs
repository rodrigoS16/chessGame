using jogo_xadres_console.Tabuleiro;
using System;

namespace jogo_xadres_console
{
    class ChessScreen
    {
        public static void PrintChessBoard(ChessBoard board)
        {
            for (int idxLine = 0; idxLine < board.Lines; idxLine++)
            {
                for (int idxColumn = 0; idxColumn < board.Columns; idxColumn++)
                {
                    if (board.GetPiece(idxLine,idxColumn) != null)
                    {
                        Console.Write($"{board.GetPiece(idxLine, idxColumn).ToString()} ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
