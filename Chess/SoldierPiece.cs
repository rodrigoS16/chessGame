using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;
using System;

namespace Chess
{
    class SoldierPiece : Piece
    {
        private ChessMatch _Math;

        public SoldierPiece(ChessBoard chessBoard, Color color, ChessMatch match, bool inverseSearch = false) : base(chessBoard, color, inverseSearch)
        {
            _Math = match;
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

            //if (Position.Line != idxL && Position.Column != idxC)
            {
                position = new Position(idxL, idxC);

                if (ChessBoard.IsPositionValid(position))
                {
                    Piece piece = ChessBoard.GetPiece(position);
                    mat[position.Line, position.Column] = piece == null || piece.Color != Color;                    
                }
            }            
        }

        public bool GetInverseSearch()
        {
            return InverseSearch;
        }

        public override bool[,] PossibleMoviments(Position pos = null)
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            int posLine = Position != null ? Position.Line : pos.Line;
            int posColumn = Position != null ? Position.Column : pos.Column;
            Position position;

            if (InverseSearch) // line 3
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
                //#Special Player EnPassant
                position = new Position(Position.Line, Position.Column-1);

                if (Position.Line == 3
                    && ChessBoard.IsPositionValid(position)
                    && ChessBoard.GetPiece(position) != null
                    && ChessBoard.GetPiece(position).Color != Color
                    && _Math.IsVulnerableEnPassant == ChessBoard.GetPiece(position))
                {
                    mat[position.Line - 1, position.Column] = true;
                }

                //#Special Player EnPassant
                position = new Position(Position.Line, Position.Column + 1);

                if (Position.Line == 3
                    && ChessBoard.IsPositionValid(position)
                    && ChessBoard.GetPiece(position) != null
                    && ChessBoard.GetPiece(position).Color != Color
                    && _Math.IsVulnerableEnPassant == ChessBoard.GetPiece(position))
                {
                    mat[position.Line - 1, position.Column] = true;
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

                //#Special Player EnPassant
                position = new Position(Position.Line, Position.Column - 1);

                if (Position.Line == 4
                    && ChessBoard.IsPositionValid(position)
                    && ChessBoard.GetPiece(position) != null
                    && ChessBoard.GetPiece(position).Color != Color
                    && _Math.IsVulnerableEnPassant == ChessBoard.GetPiece(position))
                {
                    mat[position.Line + 1, position.Column] = true;
                }

                //#Special Player EnPassant
                position = new Position(Position.Line, Position.Column + 1);

                if (Position.Line == 4
                    && ChessBoard.IsPositionValid(position)
                    && ChessBoard.GetPiece(position) != null
                    && ChessBoard.GetPiece(position).Color != Color
                    && _Math.IsVulnerableEnPassant == ChessBoard.GetPiece(position))
                {
                    mat[position.Line + 1, position.Column] = true;
                }
            }
            return mat;
        }
    }
}
