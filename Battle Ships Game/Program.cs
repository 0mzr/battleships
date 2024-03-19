using System;

class BattleshipGame
{
    static char[,] board1 = new char[10, 10]; // player 1's board
    static char[,] board2 = new char[10, 10]; // player 2's board
    static void Main(string[] args)
    {
        createBoard(); // create boardsA
        int shipCount = 1; // ship counter for ease of life
        PlaceShips(board1, "board1", shipCount);
        Console.Clear(); // hide player 1's board
        shipCount = 1; // reset ship counter
        PlaceShips(board2, "board2", shipCount);
        Console.Clear(); // hide player 2's board
        Console.WriteLine("Game starts! (Press Enter)");
        Console.ReadLine();
        // Main game loop
        string result;
        while (true)
        {
            Console.Clear();
            printBoard(board2, true);
            Console.WriteLine("Player 1\'s turn.");
            result = turn(board2);
            Console.Clear(); // clear the console to reprint player 2's board
            printBoard(board2, true);
            Console.WriteLine(result);
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            
            if (winCheck(board2)) // check if player 1 has won
            {
                Console.WriteLine("Player 1 wins!");
                Console.ReadLine();
                break;
            }

            Console.Clear();
            printBoard(board1, true);
            Console.WriteLine("Player 2\'s turn.");
            result = turn(board1);
            Console.Clear(); // clear the console to reprint player 1's board
            printBoard(board1, true);
            Console.WriteLine(result);
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            if (winCheck(board1)) // check if player 2 has won
            {
                Console.WriteLine("Player 2 wins!");
                Console.ReadLine();
                break;
            }
        }
    }

    static void createBoard()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                board1[row, col] = 'O'; // 'O' is an empty space
                board2[row, col] = 'O';
            }
        }
    }

    static void printBoard(char[,] board, bool hideShips = false)
    {
        Console.WriteLine("   A B C D E F G H I J");
        for (int row = 0; row < 10; row++)
        {
            Console.Write(row + "  ");
            for (int col = 0; col < 10; col++)
            {
                if (hideShips && board[row, col] == 'S')
                {
                    Console.Write('O' + " ");
                }
                else
                {
                    Console.Write(board[row, col] + " ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void PlaceShips(char[,] board, string nameOfBoard, int num)
    {
        for (int shipNumber = 1; shipNumber <= 10; shipNumber++)
        {
            Console.Clear(); // clear the console to hide the board temporarily
            printBoard(board);
            if (nameOfBoard == "board1")
            {
                Console.WriteLine($"Player 1, place your ships. ({num}/10)");
                num++;
            }
            else
            {
                Console.WriteLine($"Player 2, place your ships. ({num}/10)");
                num++;
            }

            bool validPlacement = false;
            while (!validPlacement)
            {
                Console.Write($"Enter the starting position (e.g. A3): ");
                string input = Console.ReadLine().ToUpper();

                if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
                {
                    Console.WriteLine("Invalid input. Use the format: LetterNumber (e.g. A3).");
                    continue;
                }

                int row = input[1] - '0';
                int col = input[0] - 'A';

                if (row < 0 || row >= 10 || col < 0 || col >= 10)
                {
                    Console.WriteLine("Invalid position. Row must be between A and F and column must be between 0 and 9. ");
                    continue;
                }

                if (board[row, col] == 'O')
                {
                    board[row, col] = 'S'; // 'S' represents a ship
                    validPlacement = true;
                }
                else
                {
                    Console.WriteLine("Invalid placement. Cell is already occupied by a ship.");
                    Console.ReadLine();
                }
            }
        }
    }

    static string turn(char[,] tboard)
    {
        int row;
        int col;
        while (true)
        {
            Console.Write("Enter target position (e.g., A3): ");
            string position = Console.ReadLine().ToUpper();

            if (position.Length != 2 || !char.IsLetter(position[0]) || !char.IsDigit(position[1]))
            {
                Console.WriteLine("Invalid input. Use the format: Letter, Number (e.g., A3).");
                continue;
            }

            row = position[1] - '0';
            col = position[0] - 'A';

            if (row < 0 || row >= 10 || col < 0 || col >= 10) 
            {
                Console.WriteLine("Invalid position. Row must be between A and F and column must be between 0 and 9.");
                continue;
            }

            if (tboard[row, col] == 'S')
            {
                tboard[row, col] = 'X'; // "X" is hit
                return "Hit!";
            }
            else if (tboard[row, col] == 'X')
            {
                Console.WriteLine("You've already hit this position. Try again.");
            }
            else if (tboard[row, col] == 'O')
            {
                tboard[row, col] = 'M'; // "M" is a miss
                return "Miss!";
            }
            else
            {
                Console.WriteLine("Invalid position. You can't hit this spot again.");
            }
        }
    }

    static bool winCheck(char[,] board)
    {
        foreach (var cell in board)
        {
            if (cell == 'S')
            {
                return false; // Not all ships are sunk
            }
        }
        return true; // All ships have been sunk
    }
}