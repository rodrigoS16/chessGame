using Chess;
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
                Console.Write( 8 - idxLine + " ");
                for (int idxColumn = 0; idxColumn < board.Columns; idxColumn++)
                {                    
                    PrintPiece(board.GetPiece(idxLine, idxColumn));                    
                }
                Console.WriteLine();                
            }
            Console.WriteLine("  A B C D E F G H");
        }
        public static void PrintChessBoard(ChessBoard board, bool[,] possibleMovements)
        {
            ConsoleColor aux = Console.BackgroundColor;

            for (int idxLine = 0; idxLine < board.Lines; idxLine++)
            {
                Console.Write(8 - idxLine + " ");
                for (int idxColumn = 0; idxColumn < board.Columns; idxColumn++)
                {
                    if (possibleMovements[idxLine, idxColumn])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;                        
                    }
                    PrintPiece(board.GetPiece(idxLine, idxColumn));
                    Console.BackgroundColor = aux;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];

            int line = int.Parse(s[1].ToString());

            return new ChessPosition(column,line);
        }

        public static void PrintPiece( Piece piece)
        {
            ConsoleColor aux = Console.ForegroundColor;

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                switch (piece.Color)
                {
                    case Tabuleiro.Enums.Color.White:
                        Console.ForegroundColor = aux;
                        break;

                    case Tabuleiro.Enums.Color.Black:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;

                    case Tabuleiro.Enums.Color.Gray:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case Tabuleiro.Enums.Color.Yellow:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case Tabuleiro.Enums.Color.Red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Tabuleiro.Enums.Color.Green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Tabuleiro.Enums.Color.Orange:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                }
                Console.Write($"{piece} ");
                Console.ForegroundColor = aux;
            }
        }
    }
}
