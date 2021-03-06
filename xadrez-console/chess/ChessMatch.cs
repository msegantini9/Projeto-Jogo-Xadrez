﻿using System.Collections.Generic;
using xadrez_console.board;

namespace xadrez_console.chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            AddPiece();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no part in the chosen position!");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The piece chosen is not yours!");
            }
            if (!Board.Piece(pos).ThereIsPossibleMovement())
            {
                throw new BoardException("There are no possible movements for the chosen piece of origin!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).PossibleMovement(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public void UndoMovement(Position origin, Position destiny, Piece removedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecrementMovement();
            if(removedPiece != null)
            {
                Board.AddPiece(removedPiece, destiny);
                Captured.Remove(removedPiece);
            }
            Board.AddPiece(p, origin);

            // # jogada especial roque pequeno

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinyR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(destinyR);
                R.DecrementMovement();
                Board.AddPiece(R, originR);
            }

            // # jogada especial roque grande

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinyR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(destinyR);
                R.DecrementMovement();
                Board.AddPiece(R, originR);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && removedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    Board.AddPiece(pawn, posP);
                }
            }
        }

        public void PeformMove(Position origin, Position destiny)
        {
            Piece removedPiece = PeformMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, removedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece p = Board.Piece(destiny);

            // #jogada especial promocao
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destiny.Line == 0) || (p.Color == Color.Black && destiny.Line == 7))
                {
                    p = Board.RemovePiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.AddPiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                //Check = true;
            }
            else
            {
                Check = false;
            }

            if (CheckmateTest(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Shift++;
                ChangePlayer();
            }

            // #jogadaespecial en passant
            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public Piece PeformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovement();
            Piece removedPiece = Board.RemovePiece(destiny);
            Board.AddPiece(p, destiny);

            if (removedPiece != null)
            {
                Captured.Add(removedPiece);
            }

            // # jogada especial roque pequeno

            if(p is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinyR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(originR);
                R.IncrementMovement();
                Board.AddPiece(R, destinyR);
            }

            // # jogada especial roque grande

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinyR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(originR);
                R.IncrementMovement();
                Board.AddPiece(R, destinyR);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && removedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    removedPiece = Board.RemovePiece(posP);
                    Captured.Add(removedPiece);
                }
            }

            return removedPiece;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Piece> PieceInMacth(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }

        public Color Adversary(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach(Piece x in PieceInMacth(color))
            {
                if(x is King)
                {
                    return x;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if(king == null)
            {
                throw new BoardException("There is no " + color + " king on the board!");
            }

            foreach(Piece x in PieceInMacth(Adversary(color)))
            {
                bool[,] matrix = x.PossibleMoviments();

                if (matrix[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckmateTest(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach(Piece x in PieceInMacth(color))
            {
                bool[,] matrix = x.PossibleMoviments();

                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = PeformMovement(origin, destiny);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);

                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void AddNewPiece(char column, int line, Piece piece)
        {
            Board.AddPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void AddPiece()
        {
            //WHITE
            AddNewPiece('a', 1, new Rook(Board, Color.White));
            AddNewPiece('b', 1, new Knight(Board, Color.White));
            AddNewPiece('c', 1, new Bishop(Board, Color.White));
            AddNewPiece('d', 1, new Queen(Board, Color.White));
            AddNewPiece('e', 1, new King(Board, Color.White, this));
            AddNewPiece('f', 1, new Bishop(Board, Color.White));
            AddNewPiece('g', 1, new Knight(Board, Color.White));
            AddNewPiece('h', 1, new Rook(Board, Color.White));
            AddNewPiece('a', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('b', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('c', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('d', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('e', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('f', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('g', 2, new Pawn(Board, Color.White, this));
            AddNewPiece('h', 2, new Pawn(Board, Color.White, this));

            //BLACK
            AddNewPiece('a', 8, new Rook(Board, Color.Black));
            AddNewPiece('b', 8, new Knight(Board, Color.Black));
            AddNewPiece('c', 8, new Bishop(Board, Color.Black));
            AddNewPiece('d', 8, new Queen(Board, Color.Black));
            AddNewPiece('e', 8, new King(Board, Color.Black, this));
            AddNewPiece('f', 8, new Bishop(Board, Color.Black));
            AddNewPiece('g', 8, new Knight(Board, Color.Black));
            AddNewPiece('h', 8, new Rook(Board, Color.Black));
            AddNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            AddNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
