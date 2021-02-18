using System;
using xadrez_console.board;

namespace xadrez_console.chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            AddPiece();
            Finished = false;
        }

        public void PeformMove(Position origin, Position destiny)
        {
           PeformMovement(origin, destiny);
           Shift++;
           ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if(Board.Piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição escolhida!");
            }
            if(CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("A peça escolhida não é sua!");
            }
            if (!Board.Piece(pos).ThereIsPossibleMovement())
            {
                throw new BoardException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        public void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
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
