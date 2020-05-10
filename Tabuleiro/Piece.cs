using jogo_xadres_console.Tabuleiro.Enums;

namespace jogo_xadres_console.Tabuleiro
{
    abstract class Piece
    {        
        public Color Color { get; set; }
        public int QtdMoviments { get; protected set; }

        protected bool InverseSearch;

        public Position Position { get; set; }        

        public ChessBoard ChessBoard { get; protected set; }

        public Piece(ChessBoard chessBoard, Color color)
        {
            Color = color;                        
            ChessBoard = chessBoard;            

            this.init();
        }

        public Piece(ChessBoard chessBoard, Color color, bool inverseSearch = false) : this(chessBoard,color)
        {
            InverseSearch = inverseSearch;
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
        
        public bool ExistsPossibleMoviments()
        {
            bool[,] mat = PossibleMoviments();
            bool ret = false;

            for (int idxL = 0; idxL < ChessBoard.Lines; idxL++)
            {
                for (int idxC = 0; idxC < ChessBoard.Columns; idxC++)
                {
                    ret = mat[idxL, idxC];
                    if (ret)
                    {
                        break;
                    }
                }
                if (ret)
                {
                    break;
                }
            }
            return ret;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoviments()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMoviments(Position pos = null);
    }
}
