using xadrez_console.board;

namespace xadrez_console.chess
{
    class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
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
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(p2) && Clear(p2) && Board.ValidPosition(pos) && Clear(pos) && NumberOfMovements == 0)
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

                // #jogadaespecial en passant
                if (Position.Line == 3)
                {
                    Position esquerda = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(esquerda) && ThereIsEnemy(esquerda) && Board.Piece(esquerda) == Match.VulnerableEnPassant)
                    {
                        matrix[esquerda.Line - 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(direita) && ThereIsEnemy(direita) && Board.Piece(direita) == Match.VulnerableEnPassant)
                    {
                        matrix[direita.Line - 1, direita.Column] = true;
                    }
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
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(p2) && Clear(p2) && Board.ValidPosition(pos) && Clear(pos) && NumberOfMovements == 0)
                {
                    matrix[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsEnemy(pos))
                {
                    matrix[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4)
                {
                    Position esquerda = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(esquerda) && ThereIsEnemy(esquerda) && Board.Piece(esquerda) == Match.VulnerableEnPassant)
                    {
                        matrix[esquerda.Line + 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(direita) && ThereIsEnemy(direita) && Board.Piece(direita) == Match.VulnerableEnPassant)
                    {
                        matrix[direita.Line + 1, direita.Column] = true;
                    }
                }
            }

            return matrix;
        }
    }
}