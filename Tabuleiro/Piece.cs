using jogo_xadres_console.Tabuleiro.Enums;

namespace jogo_xadres_console.Tabuleiro
{
    class Piece
    {        
        public Color Color { get; set; }
        public int QtdMoviments { get; protected set; }

        public Position Position { get; protected set; }        

        public ChessBoard ChessBoard { get; protected set; }

        public Piece(Color color, Position position, ChessBoard chessBoard)
        {
            Color = color;            
            Position = position;
            ChessBoard = chessBoard;

            this.init();
        }

        public void init()
        {
            QtdMoviments = 0;
        }
    }
}
