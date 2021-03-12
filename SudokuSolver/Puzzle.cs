using System;
using System.IO;

namespace SudokuSolver2
{
    class Puzzle
    {
        private string[] rows = new string[9];
        private string fileName;
        public bool isSolved;

        
        // My constructor uses StreamReader to construct the Puzzle
        public Puzzle(string fileName)
        {
            this.fileName = fileName;
            string line;
            int i = 0;

            TextReader reader = new StreamReader(fileName);
            while (true)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                this.rows[i] = line;
                i += 1;
            }
        }

        public void PuzzleConsolePrinter()
        {
            int columnID = 0; // This variable represents each column being displayed to the console
            char rowID = 'A'; // Label for each row being displayed to the console
            Console.Write("\t     1 2 3    4 5 6    7 8 9\n\t     -----------------------\n");
            while (columnID < 9)
            {
                
                if (columnID % 3 == 0 && columnID != 0) // This logic is explained on lines 39-52 of Solve.cs
                {
                    Console.WriteLine("\t     =======================");
                }
                Console.Write("\t  " + rowID + "| " );
                rowID++;
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write("|| ");
                    }

                    Console.Write(this.rows[columnID][j]);
                    Console.Write(" ");
                }
                Console.WriteLine("\t");
                columnID++;
            }
            Console.WriteLine();
        }
        // This method writes the solved puzzle to a file with a similar name to the input file. It uses StreamWriter
        public void SolutionSaver()
        {
            int row = 0;
            using (StreamWriter writer = new StreamWriter("SolutionFor" + this.fileName))
                while (row < 9)
                {
                    writer.WriteLine(this.rows[row]);
                    row++;
                }
        }
        // This method modifies the data saved to the file a bit to make it easier on the eyes
        public void PuzzleFormattedSaver()
        {
            int lineNumber = 0;
            using (StreamWriter writer = new StreamWriter("Formatted" + this.fileName))
                while (lineNumber < 9)
                {
                    writer.Write("");
                    if (lineNumber % 3 == 0 && lineNumber != 0)
                    {
                        writer.WriteLine("-------------------");
                    }
                    for (int charPos = 0; charPos < 9; charPos++)
                    {
                        if (charPos % 3 == 0 && charPos != 0)
                        {
                            writer.Write("|");
                        }
                        else if ((lineNumber == 3 || lineNumber == 6) && charPos % 9 == 0)
                        {
                            writer.Write("");
                        }
                        writer.Write(this.rows[lineNumber][charPos]);
                        writer.Write(" ");
                    }
                    writer.WriteLine("");
                    lineNumber++;
                }
        }
        // This method is used to access the rows of the puzzle from other classes.
        public string[] PuzzleCheck()
        {
            return this.rows;
        }
        // In order to test the puzzle the data needs to be worked through. As such, numbers need to be reassigned.
        // Looking back I probably shouldn't have used strings to store my data so much, their immutability made
        // modifying the puzzle more work than it needed to be.
        public void PuzzleChange(int[] pos, char c)
        {
            char[] rowChange = new char[9];
            rowChange = this.rows[pos[0]].ToCharArray();
            rowChange[pos[1]] = c;
            this.rows[pos[0]] = new string(rowChange);
        }
    }
}
