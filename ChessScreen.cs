using Chess;
using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
                SetPieceForeground(piece.Color);
                Console.Write($"{piece} ");
                Console.ForegroundColor = aux;
            }
        }
        public static void SetPieceForeground(Color     color)
        {
            switch (color)
            {
                case Color.White:                    
                    break;

                case Color.Black:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case Color.Gray:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Orange:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }
        }

        public static void PrintChessMath(ChessMatch match)
        {
            PrintChessBoard(match.ChessBoard);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Turno: {match.Turn}");

            if (!match.IsOver)
            {
                Console.WriteLine($"Aguardando jogada: {match.CurrentPlayer}");

                if (match.IsInCheck)
                {
                    Console.WriteLine("VOCÊ ESTA EM CHECK");
                }
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("XEQUEMATE!");
                builder.AppendLine($"VENCEDOR: {match.GetWinner()}");

                Console.WriteLine(builder.ToString());
            }
        }

        public static void PrintCapturedPiecesCollection(HashSet<Piece>     piecesCollection)
        {
            StringBuilder builder = new StringBuilder();
            bool firstRound = true;

            builder.Append("[");
            foreach (Piece obj   in piecesCollection)
            {
                if (!firstRound)
                {
                    builder.Append(" , ");
                }
                firstRound = false;
                builder.Append(obj);                
            }
            builder.Append("]");
            Console.WriteLine(builder.ToString());
        }

        public static void PrintCapturedPieces(ChessMatch   match)
        {
            ConsoleColor aux = Console.ForegroundColor;

            Console.WriteLine("Peças capturadas:");

            Console.Write("Branca: ");
            SetPieceForeground(Color.White);
            PrintCapturedPiecesCollection(match.PiecesCaptured(Color.White));
            Console.ForegroundColor = aux;

            Console.WriteLine();
            Console.Write("Preta: ");
            SetPieceForeground(Color.Black);
            PrintCapturedPiecesCollection(match.PiecesCaptured(Color.Black));
            Console.ForegroundColor = aux;
        }
    }
}
