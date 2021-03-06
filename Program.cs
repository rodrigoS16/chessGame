﻿using Chess;
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
                    try
                    {
                        Console.Clear();
                        ChessScreen.PrintChessMath(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position orig = ChessScreen.ReadChessPosition().ToPosition();
                        match.ValidatePieceMovementFrom(orig);

                        bool[,] possibleMovements = match.ChessBoard.GetPiece(orig).PossibleMoviments();


                        Console.Clear();
                        ChessScreen.PrintChessBoard(match.ChessBoard, possibleMovements);
                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position dest = ChessScreen.ReadChessPosition().ToPosition();
                        match.ValidatePieceMovementTo(orig,dest);

                        match.ExecutePieceMovement(orig, dest);
                    }
                    catch(ChessBoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                ChessScreen.PrintChessMath(match);

                Console.WriteLine();
            }
            catch (ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
