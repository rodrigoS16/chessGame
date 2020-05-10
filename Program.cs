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
            

            try
            {
                ChessMatch match = new ChessMatch();
                
                while (!match.IsOver)
                {
                    Console.Clear();
                    ChessScreen.PrintChessBoard(match.Tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position orig = ChessScreen.ReadChessPosition().ToPosition();
                    bool[,] possibleMovements = match.Tab.GetPiece(orig).PossibleMoviments();


                    Console.Clear();
                    ChessScreen.PrintChessBoard(match.Tab, possibleMovements);
                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position dest = ChessScreen.ReadChessPosition().ToPosition();

                    match.ProcessPieceMovement(orig, dest);
                }
                

                Console.WriteLine();
                //ChessPosition pos = new ChessPosition('c', 7);
                //Console.WriteLine(pos);
                //Console.WriteLine(pos.ToPosition());
            }
            catch (ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
