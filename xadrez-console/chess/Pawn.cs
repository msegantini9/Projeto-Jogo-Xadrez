using xadrez_console.board;

namespace xadrez_console.chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ThereIsEnemy(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Clear(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos) && NumberOfMovements == 0)
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos) && NumberOfMovements == 0)
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }
            }

            return matrix;
        }
    }
}