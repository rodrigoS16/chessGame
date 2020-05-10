using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class TowerPiece : Piece
    {

        public TowerPiece(ChessBoard chessBoard, Color color, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {

        }

        public override string ToString()
        {
            return "T";
        }

        private bool CanMove(Position   pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            return piece == null || piece.Color != Color;
        }
        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;

            if (ChessBoard.GetPiece(posLine, posColumn + 1) != null && ChessBoard.GetPiece(posLine, posColumn + 1).Color == Color)
            {
                for (int idxC = posColumn; idxC >= 0; idxC--)
                {
                    Position position = new Position(posLine, idxC);

                    if (ChessBoard.IsPositionValid(position))
                    {
                        Piece piece = ChessBoard.GetPiece(position);
                        mat[position.Line, position.Column] = piece == null || piece.Color != Color;
                    }
                }
            }
            else
            {
                for (int idxC = 0; idxC >= posColumn; idxC++)
                {
                    Position position = new Position(posLine, idxC);

                    if (ChessBoard.IsPositionValid(position))
                    {
                        Piece piece = ChessBoard.GetPiece(position);
                        mat[position.Line, position.Column] = piece == null || piece.Color != Color;
                    }
                }
            }

            if (InverseSearch == false)
            {                
                for (int idxL = 0; idxL <= ChessBoard.Lines; idxL++)
                {
                    Position position = new Position(idxL, posColumn);

                    if (ChessBoard.IsPositionValid(position))
                    {
                        Piece piece = ChessBoard.GetPiece(position);

                        mat[position.Line, position.Column] = piece == null || piece.Color != Color;
                        if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {                
                for (int idxL = ChessBoard.Lines; idxL >= 0; idxL--)
                {
                    Position position = new Position(idxL, posColumn);

                    if (ChessBoard.IsPositionValid(position))
                    {
                        Piece piece = ChessBoard.GetPiece(position);

                        mat[position.Line, position.Column] = piece == null || piece.Color != Color;
                        if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                        {
                            break;
                        }
                    }
                }
            }
            return mat;
        }
    }

}
