using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class KingPiece : Piece
    {
        private ChessMatch _Match;
        public KingPiece(ChessBoard chessBoard,Color color, ChessMatch match, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {
            _Match = match;
        }

        private bool IsEnableToRock(Position    pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            return piece != null && piece is TowerPiece && piece.QtdMoviments == 0 && piece.Color == Color;
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
            //#SpecialPlay - roque pequeno
            if (QtdMoviments == 0 && !_Match.IsInCheck)
            {
                Position towerPosition = new Position(Position.Line, Position.Column + 3);
                Position testPos1 = new Position(Position.Line, Position.Column + 1);
                Position testPos2 = new Position(Position.Line, Position.Column + 2);

                if (IsEnableToRock(towerPosition)
                    && ChessBoard.GetPiece(testPos1) == null
                    && ChessBoard.GetPiece(testPos2) == null)
                {
                    mat[testPos2.Line, testPos2.Column] = true;
                }
            }
            //#SpecialPlay - roque grande
            if (QtdMoviments == 0 && !_Match.IsInCheck)
            {
                Position towerPosition = new Position(Position.Line, Position.Column - 4);
                Position testPos1 = new Position(Position.Line, Position.Column -1);
                Position testPos2 = new Position(Position.Line, Position.Column -2);
                Position testPos3 = new Position(Position.Line, Position.Column -3);

                if (IsEnableToRock(towerPosition)
                    && ChessBoard.GetPiece(testPos1) == null
                    && ChessBoard.GetPiece(testPos2) == null
                    && ChessBoard.GetPiece(testPos3) == null)
                {
                    mat[testPos2.Line, testPos2.Column] = true;
                }
            }

            return mat;
        }
    }

}
