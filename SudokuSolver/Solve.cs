using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    class Solve
    {
        public static bool Valid(Puzzle p, char newNumber, int[] numPosition)
        {
            // First, I had to turn the board into a usable variable so the program doesn't execute Board.boardCheck() 
            // every time it does a comparison

            string[] rows = p.PuzzleCheck();

            // This first for loop checks to see if the attempted input number already exists within the row.

            for (int i = 0; i < 9; i++)
            {
                if(rows[numPosition[0]][i] == newNumber && numPosition[1] != i) // the pos[1] != i ensures it doesn't compare against itself
                {
                    return false; // If this is reached, the number existed in the row and it would be an invalid solution
                }
            }
            
            // This for loop checks columns. It accesses the column by essentially checking every position matching the
            // changed number within each string in rows.

            for (int row = 0; row < 9; row++) // I found it helps to think of numPosition[0] as the row and [1] as column
            {
                if(rows[row][numPosition[1]] == newNumber && numPosition[0] != row)
                {
                    return false;
                }
            }

            // Now checking boxes was a bit harder. Initially I created a method to create a string representing each box,
            // stored within another string[]. This took up extra memory and a few hours of my time and in the end only
            // made checking these values that much harder. As such I looked a bit deeper into a way to logic through
            // this online and found a solution on youtube that involved simple int divison. Go figure.

            // This new strategy involved calculating the box the changed number belonged too and then using those values to
            // access the rows. The variables below represent the x and y positions of the box

            int box_x = numPosition[1] / 3;
            int box_y = numPosition[0] / 3;
            
            // This works because integers don't store decimals and C# rounding means that, if the pos[] is less than 3 it
            // evaluates to 0. If pos[] is between 3, 4 or 5 it evaluates to 1, and 6,7,8 evaluate to 2. WAY simpler than my
            // inital code.

            // Now, we will use the calculated box position to get check against every number within that box.

            for (int i = (box_y * 3); i < (box_y *3 + 3); i++)
            {
                for (int j = (box_x * 3); j < (box_x*3 + 3); j++)
                {
                    if(rows[i][j] == newNumber && numPosition[1] != j && numPosition[0] != i)
                    {
                        return false;
                    }
                }
            }
            return true; 
            // If the puzzle managed to pass every test with the attempted number then it is a valid solution to
            // that empty position and returns true.
        }

        public static int[] emptySquare(Puzzle p)
        {
            // In the method it didn't make sense to c
            string[] rows = p.PuzzleCheck();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (rows[i][j] == '0')
                    {
                        return new int[] { i, j }; // The most efficient way to return this array of the positions
                    }
                }
            }
            // And, of course, because the method must return something, it will return an impossible position if the puzzle is
            // solved. Since the positions within my string[] only go up to 8(9 rows in a sudoku, 0-8), 9 would be impossible.
            return new int[] { 9 };
        }

        // And finally, to write the algorithm that uses these methods to solve a Sudoku puzzle:

        public static bool solve(Puzzle p)
        {
            // First, create your rows
            string[] rows = p.PuzzleCheck();

            // Then, find an empty square:
            int[] emptyPosition = emptySquare(p);

            // Make sure the puzzle isn't completely solved by using our exit case:
            if (emptyPosition[0] == 9)
            {
                // If it is,  save the unformatted version as SolutionFor(inputfilename)
                p.SolutionSaver();
                return false;
            }

            char c = '0';
            for (int i = 1; i < 10; i++)
            {
                c++;
                if (Valid(p, c, emptyPosition))
                {
                    p.PuzzleChange(emptyPosition, c);
                    if (Valid(p, c, emptyPosition))
                    {
                        solve(p);
                    }
                    p.PuzzleChange(emptyPosition, '0');
                }
            }
            return false;

        }
    }
}
