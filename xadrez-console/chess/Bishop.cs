using xadrez_console.board;

namespace xadrez_console.chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
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

            //NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            //NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            //SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            //SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }

            return matrix;
        }
    }
}
