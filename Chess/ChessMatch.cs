﻿using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;
using System.Collections.Generic;
using Tabuleiro.Exception;

namespace Chess
{
    class ChessMatch
    {
        public ChessBoard ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsOver { get; private set; }
        public bool IsInCheck { get; private set; }

        private HashSet<Piece> _Pieces;

        private HashSet<Piece> _PiecesCaptured;
        

        public ChessMatch()
        {
            Init();            
        }
        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }
        private void InitChessBoard()
        {            
            PieceMovement('c', 1, new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('c', 2, new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('d', 2, new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('e', 2, new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('e', 1, new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('d', 1, new KingPiece(ChessBoard, Color.White, true));

            PieceMovement('c', 8, new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('c', 7, new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('d', 7, new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('e', 7, new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('e', 8, new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('d', 8, new KingPiece(ChessBoard, Color.Black));
        }

        private void Init()
        {
            ChessBoard = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsOver = false;
            _Pieces = new HashSet<Piece>();
            _PiecesCaptured = new HashSet<Piece>();
            IsInCheck = false;

            InitChessBoard();
        }
        private Piece GetKingPiece(Color    color)
        {
            Piece piece = null;
            
            foreach(Piece obj in PiecesInMatch(color))
            {
                if (obj is KingPiece)
                {
                    piece = obj;
                    break;
                }
            }
            return piece;
        }

        private void CheckFailed(string msg)
        {
            throw new ChessBoardException(msg);
        }

        public bool CheckKingInCheck(Color color)
        {
            Piece piece = GetKingPiece(color);
            bool ret = false;

            if (piece == null)
            {
                CheckFailed($"Não tem rei da cor {color} no tabuleiro!");
            }

            foreach(Piece obj in PiecesInMatch(GetOpponent(color)))
            {
                if (obj.PossibleMoviments()[piece.Position.Line,piece.Position.Column])
                {
                    ret = true;
                }
            }
            return ret;
        }

        private Color GetOpponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private void UndoneMovement(Position orig, Position dest, Piece capturedPiece)
        {
            Piece piece = ChessBoard.RemovePiece(dest);
            
            piece.DecreaseMovement();

            if (capturedPiece != null)
            {
                ChessBoard.PutNewPiece(capturedPiece, dest);
                _PiecesCaptured.Remove(capturedPiece);
            }
            ChessBoard.PutNewPiece(piece, orig);
        }

        public void ExecutePieceMovement(Position orig, Position dest)
        {                                    
            Piece   piece = ProcessPieceMovement(orig, dest);

            if (CheckKingInCheck(CurrentPlayer))
            {
                UndoneMovement(orig, dest, piece);

                CheckFailed("Você não pode se colocar em xeque! ");
            }

            IsInCheck = CheckKingInCheck(GetOpponent(CurrentPlayer));
            Turn++;

            ChangePlayer();


        }

        public void ValidatePieceMovementFrom(Position pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            if (piece == null)
            {
                CheckFailed("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != piece.Color)
            {
                CheckFailed("A peça de origem escolhida não é sua!");
            }
            if (!piece.ExistsPossibleMoviments())
            {
                CheckFailed("Não há movimentos possíveis para a peça de origem escolhida!");
            }

        }

        public void ValidatePieceMovementTo(Position orig, Position dest)
        {
            if (!ChessBoard.GetPiece(orig).CanMoveTo(dest))
            {
                CheckFailed("Posição de destino Inválida!");
            }
        }
        

        public Piece ProcessPieceMovement(Position orig, Position dest)
        {
            Piece pieceCaptured = null;

            if (ChessBoard.GetPiece(orig).PossibleMoviments()[dest.Line, dest.Column] == true)
            {
                Piece piece = ChessBoard.RemovePiece(orig);
                piece.IncreaseMovement();
                pieceCaptured = ChessBoard.RemovePiece(dest);
                ChessBoard.PutNewPiece(piece, dest);

                if (pieceCaptured != null)
                {
                    _PiecesCaptured.Add(pieceCaptured);
                }
            }
            return pieceCaptured;
        }
        
        public void PieceMovement(char column, int line, Piece piece)
        {
            ChessBoard.PutNewPiece(piece, new ChessPosition(column, line).ToPosition());
            _Pieces.Add(piece);
        }

        public HashSet<Piece> PiecesCaptured(Color  color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece obj in _PiecesCaptured)
            {
                if (obj.Color == color)
                {
                    aux.Add(obj);
                }                
            }
            return aux;
        }

        public HashSet<Piece> PiecesInMatch(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece obj in _Pieces)
            {
                if (obj.Color == color)
                {
                    aux.Add(obj);
                }
            }
            aux.ExceptWith(PiecesCaptured(color));
            return aux;
        }

        
    }
}
