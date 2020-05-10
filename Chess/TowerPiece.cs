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
        
        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;            

            for (int idxC = posColumn; idxC >= 0; idxC--)
            {
                if (Position.Line == posLine && Position.Column == idxC)
                {
                    continue;
                }

                Position position = new Position(posLine, idxC);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        break;
                    }
                }
            }

            for (int idxC = posColumn+1; idxC <= ChessBoard.Columns; idxC++)
            {
                if (Position.Line == posLine && Position.Column == idxC)
                {
                    continue;
                }
                Position position = new Position(posLine, idxC);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        break;
                    }
                }
            }
            //----------------LINES---------------------
            for (int idxL = posLine; idxL >= 0; idxL--)
            {
                if (Position.Line == idxL && Position.Column == posColumn)
                {
                    continue;
                }

                Position position = new Position(idxL, posColumn);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        break;
                    }
                    if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                    {
                        break;
                    }
                }
            }

            for (int idxL = posLine + 1; idxL <= ChessBoard.Lines; idxL++)
            {
                if (Position.Line == idxL && Position.Column == posColumn)
                {
                    continue;
                }
                Position position = new Position(idxL, posColumn);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        break;
                    }
                    if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                    {
                        break;
                    }
                }
            }            
            return mat;
        }
    }

}
