using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SudokuChecker
{
    public class Program
    {
        /*
            check for duplicate numbers on each row
            check for duplicate numbers on each column
            check for duplicate numbers on each box
         */

        static void Main(string[] args)
        {
            Console.WriteLine(isValidSudoku(GetSudokuBoardFromFile()));
            Console.ReadLine();

        }

        public static bool isValidSudoku(string[][] board)
        {
            try
            {
                for (var i = 0; i < 9; i++)
                {
                    var row = new Dictionary<char, bool>();
                    var column = new Dictionary<char, bool>();
                    var square = new Dictionary<char, bool>();

                    for (var j = 0; j < 9; j++)
                    {
                        // return false if row or column empty
                        if (board[i][j] == "" || board[i][j] == "")
                            return false;

                        var columnValue = Convert.ToChar(board[i][j]);
                        var rowValue = Convert.ToChar(board[j][i]);

                        // collect box value
                        var squareValue = Convert.ToChar(board[3 * (i / 3) + j / 3][3 * (i % 3) + j % 3]);

                        // check for column duplicate
                        if (columnValue != '\0' && column.ContainsKey(columnValue))
                        {
                            return false;
                        }
                        column.Add(columnValue, true);

                        // check for row duplicate
                        if (rowValue != '\0' && row.ContainsKey(rowValue))
                        {
                            return false;
                        }
                        row.Add(rowValue, true);

                        // check for box duplicate
                        if (squareValue != '\0' && square.ContainsKey(squareValue))
                        {
                            return false;
                        }
                        square.Add(squareValue, true);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                // something bad with board, return false
                return false;
            }

        }


        public static string[][] GetSudokuBoardFromFile(string filePath = null)
        {
            var path = string.Empty;
            var board = new char[9][];
            if (filePath == null)
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"input_sudoku.txt");
            }
            else
            {
                path = filePath;
            }

            // Get lines and create rows
            string[] lines = File.ReadAllLines(path);
            var rows = new string[9][];
            for (int i = 0; i < lines.Length; i++)
            {
                rows[i] = lines[i].Split(',');
            }
            return rows;
        }
    }
}