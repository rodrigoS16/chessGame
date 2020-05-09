using jogo_xadres_console.Tabuleiro.Enums;

namespace jogo_xadres_console.Tabuleiro
{
    class Piece
    {        
        public Color Color { get; set; }
        public int QtdMoviments { get; protected set; }

        public Position Position { get; set; }        

        public ChessBoard ChessBoard { get; protected set; }

        public Piece(ChessBoard chessBoard, Color color)
        {
            Color = color;                        
            ChessBoard = chessBoard;

            this.init();
        }

        public void init()
        {
            Position = null;
            QtdMoviments = 0;
        }

        public void IncreaseMovement()
        {
            QtdMoviments++;
        }
    }
}
