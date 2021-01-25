using System;
using xadrez_console.board;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(3, 4);

            Console.WriteLine("Position: {0}", p);

            Board board = new Board(8, 8);
        }
    }
}
