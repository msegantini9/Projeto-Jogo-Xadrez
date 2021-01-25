using xadrez_console.board;

namespace xadrez_console.chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
