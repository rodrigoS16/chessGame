using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;
using Tabuleiro.Exception;

namespace Chess
{
    class ChessMatch
    {
        public ChessBoard ChessBoard { get; private set; }

        public int Turn { get; private set; }

        public Color CurrentPlayer { get; private set; }
        public bool IsOver { get; private set; }

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
            }
        }

        private void Init()
        {
            ChessBoard = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsOver = false;
            InitChessBoard();
        }

        private void InitChessBoard()
        {
            // init white pieces
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.White,true), new ChessPosition('c', 1).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.White,true), new ChessPosition('c', 2).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.White,true), new ChessPosition('d', 2).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.White,true), new ChessPosition('e', 2).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.White,true), new ChessPosition('e', 1).ToPosition());
            ChessBoard.PutNewPiece(new KingPiece(ChessBoard, Color.White,true), new ChessPosition('d', 1).ToPosition());

            //init black pieces
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.Black), new ChessPosition('c', 8).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.Black), new ChessPosition('c', 7).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.Black), new ChessPosition('d', 7).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.Black), new ChessPosition('e', 7).ToPosition());
            ChessBoard.PutNewPiece(new TowerPiece(ChessBoard, Color.Black), new ChessPosition('e', 8).ToPosition());
            ChessBoard.PutNewPiece(new KingPiece(ChessBoard, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
