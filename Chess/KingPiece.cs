using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;

namespace Chess
{
    class KingPiece : Piece
    {

        public KingPiece(ChessBoard chessBoard,Color color) : base(chessBoard, color)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }

}
