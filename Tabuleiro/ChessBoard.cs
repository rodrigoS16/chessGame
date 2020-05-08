namespace jogo_xadres_console.Tabuleiro
{
    class ChessBoard
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        public Piece[,] Pieces { get; set; }

        public ChessBoard(int lines, int columns, Piece[,] pieces)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines,Columns];
        }
    }
}
