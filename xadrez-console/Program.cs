﻿using System;
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
                Board board = new Board(8, 8);

                board.AddPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.AddPiece(new Rook(board, Color.Black), new Position(1, 3));
                board.AddPiece(new King(board, Color.Black), new Position(0, 2));

                board.AddPiece(new Rook(board, Color.White), new Position(3, 5));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
