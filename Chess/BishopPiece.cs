using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class BishopPiece : Piece
    {

        public BishopPiece(ChessBoard chessBoard, Color color, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        private void RunColumnsMovement(bool[,]     mat,                                        
                                        int         idxL, 
                                        int         idxCPlus,
                                        int         idxMinus,
                                        ref bool    isBreakedPlus,
                                        ref bool    isBreakedMinus)
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
            int idxCMinus = posColumn-1;
            int idxCPlus = posColumn+1;
            bool isBreakedPlus = false;
            bool isBreakedMinus = false;                        

            for (int idxL = posLine-1; idxL >= 0; idxL--)
            {                
                
                RunColumnsMovement(mat, idxL, idxCPlus, idxCMinus, ref isBreakedPlus, ref isBreakedMinus);
                idxCPlus++;
                idxCMinus--;
            }
            isBreakedPlus = false;
            isBreakedMinus = false;
            idxCMinus = posColumn - 1;
            idxCPlus = posColumn + 1;

            for (int idxL = posLine + 1; idxL <= ChessBoard.Lines; idxL++)
            {                                
                RunColumnsMovement(mat, idxL, idxCPlus, idxCMinus, ref isBreakedPlus, ref isBreakedMinus);
                idxCPlus++;
                idxCMinus--;
            }
            
            return mat;
        }
    }
}
