using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class SoldierPiece : Piece
    {
        public SoldierPiece(ChessBoard chessBoard, Color color, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {

        }

        public override string ToString()
        {
            return "S";
        }

        private void RunColumnsMovement(bool[,] mat,
                                        int idxL,
                                        int idxC)
        {
            Position position;

            if (Position.Line != idxL && Position.Column != idxC)
            {
                position = new Position(idxL, idxC);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;                    
                }
            }            
        }

        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;
            Position position;

            if (InverseSearch)
            {
                RunColumnsMovement(mat, posLine - 1, posColumn);

                if (QtdMoviments == 0)
                {
                    RunColumnsMovement(mat, posLine - 2, posColumn);
                }

                position = new Position(posLine - 1, posColumn - 1);
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                {
                    RunColumnsMovement(mat, position.Line, position.Column);
                }

                position = new Position(posLine - 1, posColumn + 1);
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                {
                    RunColumnsMovement(mat, position.Line, position.Column);
                }
            }
            else
            {
                RunColumnsMovement(mat, posLine + 1, posColumn);

                if (QtdMoviments == 0)
                {
                    RunColumnsMovement(mat, posLine + 2, posColumn);
                }

                position = new Position(posLine + 1, posColumn - 1);
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                {
                    RunColumnsMovement(mat, position.Line, position.Column);
                }

                position = new Position(posLine + 1, posColumn + 1);
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                {
                    RunColumnsMovement(mat, position.Line, position.Column);
                }
            }
            return mat;
        }
    }
}
