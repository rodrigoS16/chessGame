using jogo_xadres_console.Tabuleiro;
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

        private HashSet<Piece> _Pieces;
        private HashSet<Piece> _PiecesCaptured;

        public ChessMatch()
        {
            Init();            
        }

        public void ExecutePieceMovement(Position orig, Position dest)
        {
            ProcessPieceMovement(orig, dest);
            Turn++;
            ChangePlayer();
        }

        public void ValidatePieceMovementFrom(Position pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            if (piece == null)
            {
                throw new ChessBoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != piece.Color)
            {
                throw new ChessBoardException("A peça de origem escolhida não é sua!");
            }
            if (!piece.ExistsPossibleMoviments())
            {
                throw new ChessBoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }

        }

        public void ValidatePieceMovementTo(Position orig, Position dest)
        {
            if (!ChessBoard.GetPiece(orig).CanMoveTo(dest))
            {
                throw new ChessBoardException("Posição de destino Inválida!");
            }
        }

        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public void ProcessPieceMovement(Position orig, Position dest)
        {
            if (ChessBoard.GetPiece(orig).PossibleMoviments()[dest.Line, dest.Column] == true)
            {
                Piece piece = ChessBoard.RemovePiece(orig);
                piece.IncreaseMovement();
                Piece pieceCaptured = ChessBoard.RemovePiece(dest);
                ChessBoard.PutNewPiece(piece, dest);

                if (pieceCaptured != null)
                {
                    _PiecesCaptured.Add(pieceCaptured);
                }
            }
        }

        private void Init()
        {
            ChessBoard = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsOver = false;
            _Pieces = new HashSet<Piece>();
            _PiecesCaptured = new HashSet<Piece>();

            InitChessBoard();
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

        private void InitChessBoard()
        {
            // init white pieces
            PieceMovement('c', 1,new TowerPiece(ChessBoard, Color.White, true));
            PieceMovement('c', 2,new TowerPiece(ChessBoard, Color.White,true));
            PieceMovement('d', 2,new TowerPiece(ChessBoard, Color.White,true));
            PieceMovement('e', 2,new TowerPiece(ChessBoard, Color.White,true));
            PieceMovement('e', 1,new TowerPiece(ChessBoard, Color.White,true));
            PieceMovement('d', 1,new KingPiece(ChessBoard, Color.White,true));
            
            PieceMovement('c', 8,new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('c', 7,new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('d', 7,new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('e', 7,new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('e', 8,new TowerPiece(ChessBoard, Color.Black));
            PieceMovement('d', 8, new KingPiece(ChessBoard, Color.Black));
        }
    }
}
