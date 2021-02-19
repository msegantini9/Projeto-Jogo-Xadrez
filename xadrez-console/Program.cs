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
                    try
                    {

                        Console.Clear();
                        Screen.PrintMacth(chessMatch);

                        Console.WriteLine();

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateOriginPosition(origin);
                        bool[,] PossiblePositions = chessMatch.Board.Piece(origin).PossibleMoviments();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, PossiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateDestinyPosition(origin, destiny);

                        chessMatch.PeformMove(origin, destiny);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
