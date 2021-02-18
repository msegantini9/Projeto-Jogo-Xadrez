using System;
using xadrez_console.board;
using xadrez_console.chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);

                    Console.WriteLine();
                        
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destino: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    chessMatch.PeformMovement(origin, destiny);
                }
            }
            catch (BoardException e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
