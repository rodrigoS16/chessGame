using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class ChessMatch
    {
        public ChessBoard Tab { get; private set; }

        private int Turn;

        private Color CurrentPlayer;
        public bool IsOver { get; private set; }

        public ChessMatch()
        {
            Init();            
        }

        public void ProcessPieceMovement(Position orig, Position dest)
        {
            if (Tab.GetPiece(orig).PossibleMoviments()[dest.Line, dest.Column] == true)
            {
                Piece piece = Tab.RemovePiece(orig);
                piece.IncreaseMovement();
                Piece pieceCaptured = Tab.RemovePiece(dest);
                Tab.PutNewPiece(piece, dest);
            }
        }

        private void Init()
        {
            Tab = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsOver = false;
            InitChessBoard();
        }

        private void InitChessBoard()
        {
            // init white pieces
            Tab.PutNewPiece(new TowerPiece(Tab, Color.White,true), new ChessPosition('c', 1).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.White,true), new ChessPosition('c', 2).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.White,true), new ChessPosition('d', 2).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.White,true), new ChessPosition('e', 2).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.White,true), new ChessPosition('e', 1).ToPosition());
            Tab.PutNewPiece(new KingPiece(Tab, Color.White,true), new ChessPosition('d', 1).ToPosition());

            //init black pieces
            Tab.PutNewPiece(new TowerPiece(Tab, Color.Black), new ChessPosition('c', 8).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.Black), new ChessPosition('c', 7).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.Black), new ChessPosition('d', 7).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.Black), new ChessPosition('e', 7).ToPosition());
            Tab.PutNewPiece(new TowerPiece(Tab, Color.Black), new ChessPosition('e', 8).ToPosition());
            Tab.PutNewPiece(new KingPiece(Tab, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
