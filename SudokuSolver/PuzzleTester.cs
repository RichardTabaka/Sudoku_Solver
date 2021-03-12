using System;

namespace SudokuSolver2
{
    class PuzzleTester
    {
        public static void AllTests(Puzzle p)
        {
            RowTest(p);
            ColumnTest(p);
            CharTest(p);
            IsValid(p);
        }
        // The hardest error to solve was ensuring that the input puzzle didn't already have issues. For a while it simply 
        // solved through anyway. This uses IsValid() to make sure the puzzle is valid before it even starts and reuses code
        // I already had.
        static void IsValid(Puzzle p)
        {
            bool valid = true;
            char num;
            int[] numPos = new int[2];
            for(int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    num = p.PuzzleCheck()[row][col];
                    numPos = new int[] {row, col};
                    if(Solve.Valid(p, num, numPos) == false && num != '0')
                    {
                        valid = false;
                    }
                    if(valid == false)
                    {
                        Console.WriteLine("\n\n\tUh oh! It looks like your puzzle is invalid! Check to make sure every number in every box, row\n\tand column is unique to it and restart the program!");
                        Console.ReadLine();
                    }
                }
            }
        }
        // The rest of these are pretty self explanatory.
        static void RowTest(Puzzle p)
        {
            if (p.PuzzleCheck()[8] == null)
            {
                Console.WriteLine("\n\n\tUh oh! It looks like your puzzle is missing a row, check the file and restart the program before it crashes!");
                Console.ReadLine();
            }
        }

        static void ColumnTest(Puzzle p)
        {
            for(int i = 0; i < 9; i++)
            {
                if(p.PuzzleCheck()[i].Length != 9)
                {
                    Console.WriteLine("\n\n\tUh oh! It looks like one or more of your columns contains the wrong amount of numbers! Check your file\n\tand restart the program before it crashes!");
                    Console.ReadLine();
                }
            }
        }
        static void CharTest(Puzzle p)
        {
            bool bo = false;
            string s = "";
            for (int i = 0; i < 9; i++)
            {
                s = s + p.PuzzleCheck()[i];
            }
            foreach(char c in s)
            {
                if ( c < '0' || c > '9')
                {
                    bo = true;
                }
            }
            if (bo)
            {
                Console.WriteLine("\n\n\tUh oh! It looks like your puzzle contained a character that wasn't a number! Check your file and restart\n\tthe program before it crashes!");
                Console.ReadLine();
            }
        }
    }
}
