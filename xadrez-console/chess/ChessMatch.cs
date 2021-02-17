using System;
using xadrez_console.board;

namespace xadrez_console.chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Shift;
        private Color CurrentPlayer;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            AddPiece();
        }

        public void PeformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovement();
            Piece pieceRemoved = Board.RemovePiece(destiny);
            Board.AddPiece(p, destiny);
        }

        private void AddPiece()
        {
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.AddPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.AddPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());

        }
    }
}
