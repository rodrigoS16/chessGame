namespace jogo_xadres_console.Tabuleiro
{
    class ChessBoard
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        private Piece[,] _Pieces;

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _Pieces = new Piece[Lines,Columns];
        }

        public Piece GetPiece(int lines, int columns)
        {
            return _Pieces[lines, columns];
        }

        public void PutNewPiece(Piece piece, Position pos)
        {
            piece.Position = pos;
            _Pieces[piece.Position.Line, piece.Position.Column] = piece;            
        }
    }
}
