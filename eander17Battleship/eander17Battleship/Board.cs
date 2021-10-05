using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eander17Battleship
{
    /// <summary>
    /// Act as a container for a gameboard consisting of chars
    /// </summary>
    class Board
    {

        //field
        char[,] gameBoard = new char[10, 10];


        public void FillBoard(char newChar)
        {
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    gameBoard[row, col] = newChar;
                }
                
            }
        }

        public void Reset()
        {
            FillBoard(' ');
        }

        public void Display()
        {
            DrawColNums();
            DrawHorizontalLine();
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    Console.Write($" {gameBoard[row, col]} |");
                }

                Console.WriteLine();
                DrawHorizontalLine();
            }
        }

        public void DisplayHit(int r, int c)
        {
            gameBoard[r, c] = 'X'; 
        }

        public void DisplayMiss(int r, int c)
        {
            gameBoard[r, c] = 'O'; 
        }

        /// <summary>
        /// method to draw the coordinate numbers for the columns. 
        /// </summary>
        private static void DrawColNums()
        {
            Console.Write("   ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"  {i} ");
            }
            Console.WriteLine();
        }



        /// <summary>
        /// Shows the location of ships with an 'S'
        /// </summary>
        public void DisplayHacks()
        {
            DrawColNums();
            DrawHorizontalLine();
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    if(gameBoard[row, col] == ' ' || gameBoard[row, col] == 'X' || gameBoard[row, col] == 'O')
                        Console.Write($" {gameBoard[row, col]} |");
                    else
                        Console.Write(" S |");
                     
                }

                Console.WriteLine();
                DrawHorizontalLine();
            }
        }

        /// <summary>
        /// method to display the game board with no hacks enabled. 
        /// Will display blank board at beginning of game. 
        /// </summary>
        public void DisplayNoHacks()
        {
            DrawColNums();
            DrawHorizontalLine();
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    if (gameBoard[row, col] == 'X' || gameBoard[row, col] == 'O' || gameBoard[row, col] == ' ')
                        Console.Write($" {gameBoard[row, col]} |");
                    else
                        Console.Write("   |");
                   
                }

                Console.WriteLine();
                DrawHorizontalLine();
            }
        }

        public void DrawShip(char ch, int r, int c)
        {
            gameBoard[r, c] = ch; 
        }

        /// <summary>
        /// Draws a Horizontal line consisting of dashes of the appropriate length. 
        /// </summary>

        private void DrawHorizontalLine()
        {
            Console.Write("   -");
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                Console.Write("----");
            }
            Console.WriteLine("-");
        }

        /// <summary>
        /// Checks for an empty space in the given coordinate. 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool CheckEmptySpace(int r, int c)
        {
            bool answer;
           return answer = gameBoard[r,c] == ' ' ? true : false; 
        }

        public char GetCoordValue(int r, int c)
        {
            return gameBoard[r, c];
        }

        public int GetBoardLength()
        {
            return gameBoard.GetLength(0);
        }

        public int GetBoardHeight()
        {
            return gameBoard.GetLength(1); 
        }

        /// <summary>
        /// searches game board for a specific character. returns true if found, and false if none are found. 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool FindChar(char ch)
        {
            
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    if (gameBoard[row, col] == ch)
                        return true;
                }
            }
            return false; 
        }

        /// <summary>
        /// method to determine if there are any ships left. returns false if no ships are found. 
        /// </summary>
        /// <returns></returns>
        public bool FindShips()
        {
            if (!FindChar('S') && !FindChar('s') && !FindChar('D') && !FindChar('d') && !FindChar('B') && !FindChar('C'))
                return false;
            else
                return true; 
        }
    }
}
