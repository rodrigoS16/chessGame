using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class KingPiece : Piece
    {

        public KingPiece(ChessBoard chessBoard,Color color, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {

        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;
            for (int idxL = posLine - 1; idxL <= (posLine + 1); idxL++)
            {                                
                for (int idxC = posColumn - 1; idxC <= (posColumn + 1); idxC++)
                {
                    Position position = new Position(idxL, idxC);

                    if (ChessBoard.IsPositionValid(position))
                    {
                        Piece piece = ChessBoard.GetPiece(position);
                        mat[position.Line, position.Column] = piece == null || piece.Color != Color;
                    }
                }                
            }
            return mat;
        }
    }

}
