using xadrez_console.board;

namespace xadrez_console.chess
{
    class King : Piece
    {

        private ChessMatch Match;
        public King(Board board, Color color, ChessMatch macth) : base(board, color)
        {
            Match = macth;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool testTowerForRock(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.NumberOfMovements == 0;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            //N
            pos.SetValues(Position.Line - 1, Position.Column);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }
            //NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }
            //L
            pos.SetValues(Position.Line, Position.Column + 1);
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
            //S
            pos.SetValues(Position.Line + 1, Position.Column);
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
            //O
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }
            //NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.Line, pos.Column] = true;
            }
            
            // # Jogada especial : roque
            if(NumberOfMovements == 0 && !Match.Check)
            {
                //roque pequeno
                Position RookPosition1 = new Position(Position.Line, Position.Column + 3);
                if (testTowerForRock(RookPosition1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if(Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        matrix[Position.Line, Position.Column + 2] = true;
                    }
                }
            }

            // # Jogada especial : roque
            if (NumberOfMovements == 0 && !Match.Check)
            {
                //roque grande
                Position RookPosition2 = new Position(Position.Line, Position.Column - 4);
                if (testTowerForRock(RookPosition2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        matrix[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
