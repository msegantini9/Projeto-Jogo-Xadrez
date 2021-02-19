using xadrez_console.board;

namespace xadrez_console.chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            return matrix;
        }
    }
}
