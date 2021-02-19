using System;
using System.Collections.Generic;
using xadrez_console.board;
using xadrez_console.chess;

namespace xadrez_console
{
    class Screen
    {
        public static void PrintBoard(Board board)
        { 

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor modifiedColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j] == true)
                    {
                        Console.BackgroundColor = modifiedColor;
                    }
                    else
                    {
                        Console.BackgroundColor = originalColor;
                    }

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalColor;
        }

        public static void PrintMacth(ChessMatch macth)
        {
            PrintBoard(macth.Board);

            Console.WriteLine();
            PrintCapturedPieces(macth);

            Console.WriteLine();
            Console.WriteLine("Shift: " + macth.Shift);
            Console.WriteLine("Awaiting move: " + macth.CurrentPlayer);
        }

        public static void PrintCapturedPieces(ChessMatch macth)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("Whites: ");
            PrintGroup(macth.CapturedPieces(Color.White));
            Console.WriteLine();

            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintGroup(macth.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintGroup(HashSet<Piece> group)
        {
            Console.Write("[");

            foreach(Piece x in group)
            {
                Console.Write(x + ", ");
            }
            Console.Write("]");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if(piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

    }
}
