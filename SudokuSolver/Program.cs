using System;

namespace SudokuSolver2
{
    class Program
    {
        static void Main(string[] args)
        {
            // A quick explanation of the program is printed to the console:

            Console.WriteLine("\n\tWelcome to the Sudoku Solver/Helper!\n\n\tThis program takes a text based file containing a Sudoku puzzle and gives that little bit of extra help\n\tyou might need!");

            Console.WriteLine("\n\tSome important notes:\n\n \t\tYour text file MUST contain only numbers.\n\t\tYour text file should have each ROW of the Sudoku puzzle on one line in the text file.\n\t\tAny empty spaces should be replaced by a 0.\n\t\tYour textfile should be placed within the debug folder of the program.");

            Console.WriteLine("\n\n\tThat should be it! What is the name of the file containing your puzzle?\n");

            // Here, the user inputs a file name and the Puzzle is created. It is then run through the PuzzleTester tests
            // to ensure the file met the requirements so the program won't crash. Then it prints the puzzle and asks for
            // input from the user to figure out what it will do next

            string inputFile = Console.ReadLine();
            Puzzle inputPuzzle = new Puzzle(inputFile);
            Console.Clear();
            Console.WriteLine($"\n\tIt looks like the file you chose was, '{inputFile}'.");
            PuzzleTester.AllTests(inputPuzzle);

            Console.WriteLine(" The puzzle is printed below!\n");
            inputPuzzle.PuzzleConsolePrinter();

            Console.WriteLine("\n\tDo you want a hint or the solution? Enter one of the following:\n\n\tH: Hint\tS: Solution\n");
            // At this point the program needs to actually solve the puzzle before it can provide any help. It does so
            // and saves the solution to the debug folder.

            Solve.solve(inputPuzzle);
            Puzzle sol = new Puzzle("SolutionFor" + inputFile);

            // If H is selected you will be prompted for a coordinate to revel. S simply prints the solution to the console

            string inp = Console.ReadLine();
            Console.Clear();
            if (inp == "H")
            {
                while (inp == "H")
                {
                    Console.WriteLine("\n\tWhich Square would you like revealed? Enter your answer in a letter-number format like so: A1\n");
                    inputPuzzle.PuzzleConsolePrinter();
                    string strHintPos = Console.ReadLine();
                    while (strHintPos.Length != 2)
                    {
                        Console.WriteLine("\n\tOops! It looks like the format of your input was invalid! try again!");
                        strHintPos = Console.ReadLine();
                    }
                    int[] hintPos = HintPosition(strHintPos);
                    Console.WriteLine($"\n\tThe number at {strHintPos} is: " + sol.PuzzleCheck()[hintPos[0]][hintPos[1]]);

                    Console.WriteLine("\n\tHopefully that hint helped! Enter one of the following:\n\tH: Get another hint\tS: Print the solution to the console\n\tX: decide how you want your solution saved and exit\n\n");
                    inp = Console.ReadLine(); // This line allows you to get multiple hints without restarting the program
                    Console.Clear();
                }
            }
            if (inp == "S")
            {
                Console.WriteLine("\n\tThe solution to your given puzzle is:\n");
                sol.PuzzleConsolePrinter();
            }
        
        
            Console.WriteLine("\n\n\tYour solution was saved without formatting. Would you like to add the formatting to make it easier to read?\n\tY or N:\n");
            if (Console.ReadLine() == "Y")
            {
                sol.PuzzleFormattedSaver(); // This method creates a new text file that has some formatting applied
            }
        


        }
        static public int[] HintPosition(string s)
        {
            // This part converts the input coordinates to ones that relate to the data in Puzzle.rows
            int[] hintPos = new int[2] { 9, 9 };
            char c = 'A';
            int i = 0;
            while(hintPos[0] == 9 && i < 9) // The first character, being a letter, can take some iteration to find.
            {
                if(c == s[0])
                {
                    hintPos[0] = i;
                }
                else
                {
                    c++;
                }
                i++;
            }
            // Fortunately the second digit is easier.
            hintPos[1] = Convert.ToInt32(Convert.ToString(s[1])) - 1;
            return hintPos;
        }
    }
}
