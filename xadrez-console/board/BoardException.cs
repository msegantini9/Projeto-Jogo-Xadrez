using System;

namespace xadrez_console.board
{
    class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}
