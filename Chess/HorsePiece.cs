using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class HorsePiece : Piece
    {

        public HorsePiece(ChessBoard chessBoard, Color color, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        private void RunColumnsMovement(bool[,] mat,
                                        int idxL,
                                        int idxCPlus,
                                        int idxMinus,
                                        ref bool isBreakedPlus,
                                        ref bool isBreakedMinus)
        {
            Position position;
            
            if (!isBreakedMinus && (Position.Line != idxL && Position.Column != idxMinus))
            {
                position = new Position(idxL, idxMinus);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        isBreakedMinus = true;
                    }
                    if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                    {
                        isBreakedMinus = true;
                    }
                }
            }
            //++
            if (!isBreakedPlus && (Position.Line != idxL || Position.Column != idxCPlus))
            {
                position = new Position(idxL, idxCPlus);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;

                    if (piece != null && piece.Color == Color)
                    {
                        isBreakedPlus = true;
                    }
                    if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).Color != Color)
                    {
                        isBreakedPlus = true;
                    }
                }
            }
        }

        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;            
            bool isBreakedPlus = false;
            bool isBreakedMinus = false;
            
            RunColumnsMovement(mat, posLine -1, posColumn+2, posColumn-2, ref isBreakedPlus, ref isBreakedMinus);            
            RunColumnsMovement(mat, posLine -2, posColumn + 1, posColumn - 1, ref isBreakedPlus, ref isBreakedMinus);            
            RunColumnsMovement(mat, posLine + 1, posColumn + 2, posColumn - 2, ref isBreakedPlus, ref isBreakedMinus);
            RunColumnsMovement(mat, posLine + 2, posColumn + 1, posColumn - 1, ref isBreakedPlus, ref isBreakedMinus);            

            return mat;
        }
    }
}
