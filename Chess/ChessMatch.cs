using jogo_xadres_console.Tabuleiro;
using jogo_xadres_console.Tabuleiro.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using Tabuleiro.Exception;

namespace Chess
{
    class ChessMatch
    {
        public Piece IsVulnerableEnPassant { get; private set; }
        public ChessBoard ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsOver { get; private set; }
        public bool IsInCheck { get; private set; }        

        private HashSet<Piece> _Pieces;

        private HashSet<Piece> _PiecesCaptured;
        

        public ChessMatch()
        {
            Init();            
        }
        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }
        private void InitChessBoard()
        {
                        
            PieceMovement('a', 1, new TowerPiece(ChessBoard, Color.White, this, true));
            PieceMovement('b', 1, new HorsePiece(ChessBoard, Color.White, true));
            PieceMovement('c', 1, new BishopPiece(ChessBoard, Color.White, true));
            PieceMovement('d', 1, new QueenPiece(ChessBoard, Color.White, true));
            PieceMovement('e', 1, new KingPiece(ChessBoard, Color.White,this, true));
            PieceMovement('f', 1, new BishopPiece(ChessBoard, Color.White, true));
            PieceMovement('g', 1, new HorsePiece(ChessBoard, Color.White, true));
            PieceMovement('h', 1, new TowerPiece(ChessBoard, Color.White, this, true));

            PieceMovement('a', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('b', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('c', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('d', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('e', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('f', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('g', 2, new SoldierPiece(ChessBoard, Color.White,this, true));
            PieceMovement('h', 2, new SoldierPiece(ChessBoard, Color.White,this, true));

            PieceMovement('a', 8, new TowerPiece(ChessBoard, Color.Black, this));
            PieceMovement('b', 8, new HorsePiece(ChessBoard, Color.Black));
            PieceMovement('c', 8, new BishopPiece(ChessBoard, Color.Black));
            PieceMovement('d', 8, new QueenPiece(ChessBoard, Color.Black));
            PieceMovement('e', 8, new KingPiece(ChessBoard, Color.Black,this));
            PieceMovement('f', 8, new BishopPiece(ChessBoard, Color.Black));
            PieceMovement('g', 8, new HorsePiece(ChessBoard, Color.Black));
            PieceMovement('h', 8, new TowerPiece(ChessBoard, Color.Black, this));

            PieceMovement('a', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('b', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('c', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('d', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('e', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('f', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('g', 7, new SoldierPiece(ChessBoard, Color.Black,this));
            PieceMovement('h', 7, new SoldierPiece(ChessBoard, Color.Black,this));

            IsVulnerableEnPassant = null;
        }

        private void Init()
        {
            ChessBoard = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsOver = false;
            _Pieces = new HashSet<Piece>();
            _PiecesCaptured = new HashSet<Piece>();
            IsInCheck = false;

            InitChessBoard();
        }
        private Piece GetKingPiece(Color    color)
        {
            Piece piece = null;
            
            foreach(Piece obj in PiecesInMatch(color))
            {
                if (obj is KingPiece)
                {
                    piece = obj;
                    break;
                }
            }
            return piece;
        }

        private void CheckFailed(string msg)
        {
            throw new ChessBoardException(msg);
        }

        public bool CheckKingInCheck(Color color)
        {
            Piece piece = GetKingPiece(color);
            bool ret = false;

            if (piece == null)
            {
                CheckFailed($"Não tem rei da cor {color} no tabuleiro!");
            }

            foreach(Piece obj in PiecesInMatch(GetOpponent(color)))
            {
                if (obj.PossibleMoviments()[piece.Position.Line,piece.Position.Column])
                {
                    ret = true;
                }
            }
            return ret;
        }

        private Color GetOpponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private void UndoneMovement(Position orig, Position dest, Piece capturedPiece)
        {
            Piece piece = ChessBoard.RemovePiece(dest);
            
            piece.DecreaseMovement();

            if (capturedPiece != null)
            {
                ChessBoard.PutNewPiece(capturedPiece, dest);
                _PiecesCaptured.Remove(capturedPiece);
            }
            ChessBoard.PutNewPiece(piece, orig);

            //#Special play Roque
            if (piece is KingPiece &&
                (orig.Column + 2 == dest.Column))
            {
                //Piece towerPiece = ChessBoard.GetPiece();
                Position towerPositionOrig = new Position(orig.Line, orig.Column + 3);
                Position towerPositionDest = new Position(orig.Line, orig.Column + 1);
                Piece towerPiece = ChessBoard.RemovePiece(towerPositionDest);
                towerPiece.DecreaseMovement();
                ChessBoard.PutNewPiece(towerPiece, towerPositionOrig);

            }
            if (piece is KingPiece &&
                (orig.Column - 2 == dest.Column))
            {
                Position towerPositionOrig = new Position(orig.Line, orig.Column - 4);
                Position towerPositionDest = new Position(orig.Line, orig.Column - 1);
                Piece towerPiece = ChessBoard.RemovePiece(towerPositionDest);
                towerPiece.DecreaseMovement();
                ChessBoard.PutNewPiece(towerPiece, towerPositionOrig);
            }
            //#Special Play EnPassant
            //#Special play EnPassant

            if (piece is SoldierPiece
                && orig.Column != dest.Column
                && capturedPiece == IsVulnerableEnPassant)
            {
                Piece soldierPiece = ChessBoard.RemovePiece(dest);
                soldierPiece.DecreaseMovement();                

                Position position;
                if ((piece as SoldierPiece).GetInverseSearch())
                {
                    position = new Position(3, dest.Column);
                }
                else
                {
                    position = new Position(4, dest.Column);
                }
                ChessBoard.PutNewPiece(soldierPiece, position);
                
            }
        }

        public void ExecutePieceMovement(Position orig, Position dest)
        {                                    
            Piece   piece = ProcessPieceMovement(orig, dest);

            if (CheckKingInCheck(CurrentPlayer))
            {
                UndoneMovement(orig, dest, piece);

                CheckFailed("Você não pode se colocar em xeque! ");
            }

            piece = ChessBoard.GetPiece(dest);
            //#Special Promotion
            if (piece is SoldierPiece
                && ((piece as SoldierPiece).GetInverseSearch() && dest.Line == 0 
                    || !(piece as SoldierPiece).GetInverseSearch() && dest.Line == 7))
            {
                piece = ChessBoard.RemovePiece(dest);
                _Pieces.Remove(piece);

                Piece queenPiece = new QueenPiece(ChessBoard, piece.Color);
                ChessBoard.PutNewPiece(queenPiece, dest);
                _Pieces.Add(queenPiece);

            }

            IsInCheck = CheckKingInCheck(GetOpponent(CurrentPlayer));

            IsOver = IsCheckMate(GetOpponent(CurrentPlayer));

            Turn++;

            ChangePlayer();
            
            //#Special player EnPassant
            if (piece is SoldierPiece && 
                (orig.Line == dest.Line + 2 || orig.Line == dest.Line - 2))
            {
                IsVulnerableEnPassant = piece;
            }
        }

        public Color GetWinner()
        {
            return GetOpponent(CurrentPlayer);
        }

        public bool IsCheckMate(Color color)
        {
            bool ret = CheckKingInCheck(color);

            if (ret)
            {
                foreach (Piece obj in PiecesInMatch(color))
                {
                    bool[,] mat = obj.PossibleMoviments();

                    for (int idxL = 0; idxL < ChessBoard.Lines; idxL++)
                    {
                        for (int idxC = 0; idxC < ChessBoard.Columns; idxC++)
                        {
                            Position dest = new Position(idxL, idxC);
                            Position orig = obj.Position;   

                            if (ChessBoard.GetPiece(orig).PossibleMoviments()[dest.Line, dest.Column])                            
                            {
                                Piece piece = ProcessPieceMovement(orig, dest);

                                ret = CheckKingInCheck(color);
                                UndoneMovement(orig, dest, piece);

                                if (!ret)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public void ValidatePieceMovementFrom(Position pos)
        {
            Piece piece = ChessBoard.GetPiece(pos);

            if (piece == null)
            {
                CheckFailed("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != piece.Color)
            {
                CheckFailed("A peça de origem escolhida não é sua!");
            }
            if (!piece.ExistsPossibleMoviments())
            {
                CheckFailed("Não há movimentos possíveis para a peça de origem escolhida!");
            }

        }

        public void ValidatePieceMovementTo(Position orig, Position dest)
        {
            if (!ChessBoard.GetPiece(orig).CanMoveTo(dest))
            {
                CheckFailed("Posição de destino Inválida!");
            }
        }
        

        public Piece ProcessPieceMovement(Position orig, Position dest)
        {
            Piece pieceCaptured = null;

            if (ChessBoard.GetPiece(orig).PossibleMoviments()[dest.Line, dest.Column] == true)
            {
                Piece piece = ChessBoard.GetPiece(orig);

                //#Special play Roque
                if (piece is KingPiece &&
                    (orig.Column + 2 == dest.Column))
                {
                    Piece towerPiece = ChessBoard.GetPiece(new Position(orig.Line, orig.Column + 3));

                    ProcessPieceMovement(towerPiece.Position, new Position(orig.Line, orig.Column + 1));
                }
                if (piece is KingPiece &&
                    (orig.Column - 2 == dest.Column))
                {
                    Piece towerPiece = ChessBoard.GetPiece(new Position(orig.Line, orig.Column - 4));

                    ProcessPieceMovement(towerPiece.Position, new Position(orig.Line, orig.Column - 1));
                }
                ///#
                
                piece = ChessBoard.RemovePiece(orig);
                piece.IncreaseMovement();
                pieceCaptured = ChessBoard.RemovePiece(dest);
                ChessBoard.PutNewPiece(piece, dest);

                if (pieceCaptured != null)
                {
                    _PiecesCaptured.Add(pieceCaptured);
                }  
                //#Special play EnPassant
                if (piece is SoldierPiece
                    && orig.Column != dest.Column
                    && pieceCaptured == null)
                {
                    Position position;
                    if ((piece as SoldierPiece).GetInverseSearch())
                    {
                        position = new Position(dest.Line + 1, dest.Column);
                    }
                    else
                    {
                        position = new Position(dest.Line - 1, dest.Column);
                    }
                    pieceCaptured = ChessBoard.RemovePiece(position);
                    
                    if (pieceCaptured != null)
                    {
                        _PiecesCaptured.Add(pieceCaptured);
                    }
                }
            }            

            return pieceCaptured;
        }
        
        public void PieceMovement(char column, int line, Piece piece)
        {
            ChessBoard.PutNewPiece(piece, new ChessPosition(column, line).ToPosition());
            _Pieces.Add(piece);
        }

        public HashSet<Piece> PiecesCaptured(Color  color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece obj in _PiecesCaptured)
            {
                if (obj.Color == color)
                {
                    aux.Add(obj);
                }                
            }
            return aux;
        }

        public HashSet<Piece> PiecesInMatch(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece obj in _Pieces)
            {
                if (obj.Color == color)
                {
                    aux.Add(obj);
                }
            }
            aux.ExceptWith(PiecesCaptured(color));
            return aux;
        }

        
    }
}
