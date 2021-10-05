using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eander17Battleship
{
    class Game
    {
        //Variables required for game. 
        bool hacks = false;
        Random random = new Random(); 
         

        //objects
        Board board = new Board();

        Ship Destroyer1 = new Ship(2, 'D');
        Ship Destroyer2 = new Ship(2, 'd');
        Ship Sub1 = new Ship(3, 'S');
        Ship Sub2 = new Ship(3, 's');
        Ship Battleship = new Ship(4, 'B');
        Ship Carrier = new Ship(5, 'C'); 

        /// <summary>
        /// Constructor method for Game object. calls the StartGame method. 
        /// </summary>
        public Game()
        {
            StartGame();
        }

        /// <summary>
        /// Initial method for the game. is called by the constructor. 
        /// Sets up the board, sets the ships and determines whether or not hacks are to be enabled. 
        /// After initial setup, this method calls the ActualGame method. 
        /// </summary>
        private void StartGame()
        {
            Console.Clear(); 
            Console.WriteLine("BattleShip!"); 

            board.Reset();
            //board.Display();

           SetAllShipCoordinates();
            hacks = HackerMan(); 
            if (hacks)
                board.DisplayHacks();
            else
                board.DisplayNoHacks();

            ActualGame(); 
        } // end StartGame method

        /// <summary>
        /// main body of the game. is called by the StartGame function. 
        /// determines whether hacks are enabled and asks user for coordinates. 
        /// This method determines whether the game is over. 
        /// </summary>
        private void ActualGame()
        {
            bool running = true;
            while (running)
            {
                int col = AskForCoord('x');
                int row = AskForCoord('y');
                CheckForHit(row, col);

                if (hacks)
                    board.DisplayHacks();
                else
                    board.DisplayNoHacks();

                if (AllDeadShips())
                    running = false;  
            }
            GameOver(); 
        }

        private bool CheckForHit(int row, int col)
        {
            if (!board.CheckEmptySpace(row, col))
            {
                if (board.GetCoordValue(row, col) != 'X' || board.GetCoordValue(row, col) != 'O')
                {
                    Console.WriteLine("That's a hit!");
                    char shipType = board.GetCoordValue(row, col);
                    board.DrawShip('X', row, col);
                    DeadShip(shipType);
                    return true;
                }
                else
                {
                    Console.WriteLine("You already hit that coordinate..");
                    return false;
                }
            }
            else
            {
                board.DisplayMiss(row, col); 
                return false;
            }
        }

        /// <summary>
        /// method to verify whether a ship has been sunk or not. 
        /// writes a method to the console telling the user they sunk a ship and which ship it was. 
        /// </summary>
        /// <param name="shipType"></param>
        private void DeadShip(char shipType)
        {
            if(!board.FindChar(shipType))
            {
                string s = DetermineShip(shipType);
                Console.WriteLine($"You sunk my {s}!"); 
            }
        }

        /// <summary>
        /// simple method that determines what type of ship is found by checking the letter assigned to the ship object. 
        /// </summary>
        /// <param name="shipType"></param>
        /// <returns></returns>
        private string DetermineShip(char shipType)
        {
            switch(shipType)
            {
                case 'D':
                case 'd':
                    return "Destroyer";
                case 'S':
                case 's':
                    return "Submarine";
                case 'B':
                    return "Battleship";
                case 'C':
                    return "Carrier";
                default:
                    return "lol idk"; 
            }
        }

        /// <summary>
        /// Method that determines whether all the ships in the game are sunk. 
        /// </summary>
        /// <returns></returns>
        private bool AllDeadShips()
        {
            return !board.FindShips(); 
        }

        private void GameOver()
        {
            Console.WriteLine("You win!");
            Console.WriteLine("Play again? (Y/N)");
            char play = Console.ReadKey().KeyChar;
            if (play == 'y' || play == 'Y')
                StartGame(); 
        }

        //Checks to see if user wants to display hacks. 
        private bool HackerMan()
        {
            Console.Write("Activate hacks? (Y/N): ");
            char verify = Console.ReadKey().KeyChar;

            if (verify == 'Y' || verify == 'y')
            {
                Console.WriteLine("ACTIVATE HACKERMODE");
                return true; 
            }
            else
                Console.WriteLine("Hacks disabled.");
            return false; 

        } // end HackerMan method

        //method that sets the coordinates for all ships. 
        private void SetAllShipCoordinates()
        {
            SetShipCoordinates(Carrier);
            SetShipCoordinates(Battleship);
            SetShipCoordinates(Sub1);
            SetShipCoordinates(Sub2); 
            SetShipCoordinates(Destroyer1); 
            SetShipCoordinates(Destroyer2);
        } // end SetAllShipCoordinates method

        /// <summary>
        /// Method to set stern and bow of ship. 
        /// Contains a do while loop with PlaceBow() in the loop. 
        /// Continues while PlaceStern returns false. 
        /// This ensures that no part of the ship lands on an occupied space and will re-place the bow if there are no valid 
        ///     spaces for the stern. 
        /// </summary>
        /// <param name="ship"></param>
        private void SetShipCoordinates(Ship ship)
        {
            do
            {
                PlaceBow(ship);
            } while (!PlaceStern(ship));
        } // end SetShipCoordinates method

        /// <summary>
        /// places the stern of the ship. 
        /// Method randomly decides whether to go right or downwards and proceeds to check if it can be validly placed. 
        /// If the first direction fails, method proceeds to attempt the other direction. 
        /// If both directions fail, method returns false letting the program know the bow must be placed elsewhere. 
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool PlaceStern(Ship ship)
        {
            //Random random = new Random(); 

            int direction = random.Next(2);
                       
            if (direction == 0) //sets East, increasing column value
            {
                if (!SetSternEast(ship))
                    if (!SetSternSouth(ship))
                        return false;
                return true; 
            }
            else //Sets south, increasing row value
            {
                if (!SetSternSouth(ship))
                    if (!SetSternEast(ship))
                        return false;
                return true; 
            }
        }

        /// <summary>
        /// Method to check that each tile in the ship's length is an unoccupied space. 
        /// returns false if there is already an occupied space or if the ship will go off the edge. 
        /// if there is no occupied space, the method will set the stern coordinates and proceed to call the draw ship method. 
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool SetSternSouth(Ship ship)
        {
            bool verifyEmpty = true;
            if (ship.GetBowY() + ship.GetLength() <= 10)
            {
                for (int check = 1; check < ship.GetLength(); check++)
                {
                    if (board.GetCoordValue(ship.GetBowY() + check, ship.GetBowX()) != ' ')
                    {
                        return verifyEmpty = false;
                        
                    }
                }
                if (verifyEmpty == true)
                {
                    ship.SetStern(ship.GetBowY() + ship.GetLength() - 1, ship.GetBowX());
                    for (int i = 0; i < ship.GetLength(); i++)
                        board.DrawShip(ship.GetShipType(), ship.GetBowY() + i, ship.GetBowX());
                }
                else
                    return false; 
            }
            else
                verifyEmpty = false; 

            return verifyEmpty;
        }

        /// <summary>
        /// operates the same as SetSternSouth, however this method sets the stern to the right of the bow. 
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool SetSternEast(Ship ship)
        {
            bool verifyEmpty = true;
            if (ship.GetBowX() + ship.GetLength() <= 10)
            {
                for (int check = 1; check < ship.GetLength(); check++)
                {
                    if (board.GetCoordValue(ship.GetBowY(), ship.GetBowX() + check) != ' ')
                    {
                       return verifyEmpty = false;
                        
                    }

                }
                if (verifyEmpty == true)
                {
                    ship.SetStern(ship.GetBowY(), ship.GetBowX() + ship.GetLength() - 1);
                    for (int i = 0; i < ship.GetLength(); i++)
                    {
                        board.DrawShip(ship.GetShipType(), ship.GetBowY(), ship.GetBowX() + i);
                    }

                }
                else
                    return false; 
            }
            else
                verifyEmpty = false; 

            return verifyEmpty;
        }

        /// <summary>
        /// method to place the bow. Continually checks for an open space, once found bow is placed at those coordinates. 
        /// </summary>
        /// <param name="ship"></param>
        private void PlaceBow(Ship ship)
        {
            //Random random = new Random(); 

            int r, c;
            r = random.Next(10);
            c = random.Next(10);

            while(!board.CheckEmptySpace(r,c))
            {
                r = random.Next(0, 9);
                c = random.Next(0, 9);
            }
            
            ship.SetBow(r, c);
        }

        /// <summary>
        /// user input method designed to ask the user for a coordinate. 
        /// Method is designed to work for both x(column) and y(row) values by using a character parameter in the method call. 
        /// Will check for valid input, notifying the user if input is invalid where they then must input a new character. 
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        private int AskForCoord(char axis)
        {
            Console.Write($"Enter {axis} value: ");

            int x = -1;

            while (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x > 9)
            {
                Console.WriteLine("Invalid input! try again.");
            }
            return x;
        }
    }
}
