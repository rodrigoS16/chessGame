using Tabuleiro.Exception;

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
        public Piece GetPiece(Position  pos)
        {
            return GetPiece(pos.Line, pos.Column);
        }

        public bool ExistsPiece(Position pos)
        {
            PositionValidate(pos);
            return GetPiece(pos) != null;
        }

        public void PutNewPiece(Piece piece, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new ChessBoardException("Já existe uma peça nesta posição!");
            }

            piece.Position = pos;
            _Pieces[piece.Position.Line, piece.Position.Column] = piece;            
        }

        public Piece RemovePiece(Position pos)
        {
            Piece piece = null;

            if (GetPiece(pos) != null)
            {
                piece = _Pieces[pos.Line, pos.Column];
                _Pieces[piece.Position.Line, piece.Position.Column] = null;

                piece.Position = null;
            }
            return piece;
        }

        public bool IsPositionValid(Position    pos)
        {
            bool ret = true;

            if (pos.Column < 0 ||
                pos.Line < 0 ||
                Lines <= pos.Line ||
                Columns <= pos.Column )
            {
                ret = false;
            }

            return ret;
        }

        public void PositionValidate(Position pos)
        {
            if (!IsPositionValid(pos))
            {
                throw new ChessBoardException("Posição inválida!");
            }
        }
    }
}
