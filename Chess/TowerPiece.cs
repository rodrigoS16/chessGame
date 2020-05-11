using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class TowerPiece : Piece
    {
        private ChessMatch _Match;

        public TowerPiece(ChessBoard chessBoard, Color color, ChessMatch match, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {
            _Match = match;
        }

        private bool IsEnableToRock(Position pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            return piece != null && piece is KingPiece && piece.QtdMoviments == 0 && piece.Color == Color;
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
            //#SpecialPlay - roque
            if (QtdMoviments == 0 && !_Match.IsInCheck)
            {
                Position kingPosition = new Position(Position.Line, Position.Column - 3);
                Position testPos1 = new Position(Position.Line, Position.Column - 2);
                Position testPos2 = new Position(Position.Line, Position.Column - 1);

                if (IsEnableToRock(kingPosition)
                    && ChessBoard.GetPiece(testPos1) == null
                    && ChessBoard.GetPiece(testPos2) == null)
                {
                    mat[testPos1.Line, testPos1.Column] = true;
                }
            }
            //#SpecialPlay - roque grande
            if (QtdMoviments == 0 && !_Match.IsInCheck)
            {
                Position kingPosition = new Position(Position.Line, Position.Column + 4);
                Position testPos1 = new Position(Position.Line, Position.Column + 1);
                Position testPos2 = new Position(Position.Line, Position.Column + 2);
                Position testPos3 = new Position(Position.Line, Position.Column + 3);

                if (IsEnableToRock(kingPosition)
                    && ChessBoard.GetPiece(testPos1) == null
                    && ChessBoard.GetPiece(testPos2) == null
                    && ChessBoard.GetPiece(testPos3) == null)
                {
                    mat[testPos3.Line, testPos3.Column] = true;
                }
            }
            return mat;
        }
    }

}
