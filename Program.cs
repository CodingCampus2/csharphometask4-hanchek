using System;
using CodingCampusCSharpHomework;

namespace HomeworkTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<Task4, char[,]>TaskSolver = task =>
            {
                char[,] board = task.Board.Clone() as char[,];

                int n = board.GetLength(0);
                int m = board.GetLength(1);

                for(int i = 1; i < n - 1; ++i)
                {
                    for(int j = 1; j < m - 1; ++j)
                    {
                        if (CheckInnerCell(task.Board, i, j))
                        {
                            board[i, j] = '1';
                        }
                        else
                        {
                            board[i, j] = '0';
                        }
                    }
                }

                for(int i = 0; i < n; ++i)
                {
                    //check top border and corners
                    if (CheckBoundaryCell(task.Board, i, 0))
                    {
                        board[i, 0] = '1';
                    }
                    else
                    {
                        board[i, 0] = '0';
                    }
                    //check down border and corners
                    if (CheckBoundaryCell(task.Board, i, m - 1))
                    {
                        board[i, m - 1] = '1';
                    }
                    else
                    {
                        board[i, m - 1] = '0';
                    }
                }

                for(int j = 1; j < m - 1; ++j)
                {
                    //check left border
                    if (CheckBoundaryCell(task.Board, 0, j))
                    {
                        board[0, j] = '1';
                    }
                    else
                    {
                        board[0, j] = '0';
                    }
                    //check right border
                    if (CheckBoundaryCell(task.Board, n - 1, j))
                    {
                        board[n - 1, j] = '1';
                    }
                    else
                    {
                        board[n - 1, j] = '0';
                    }
                }

                return board;
            };

            Task4.CheckSolver(TaskSolver);
        }

        static bool IsAlive(bool wasAlive, int numberOfNeighbors)
        {
            if (wasAlive)
            {
                return numberOfNeighbors > 1 && numberOfNeighbors < 4;
            }
            else
            {
                return numberOfNeighbors == 3;
            }
        }

        static bool AreIndicesValid(char[,] board, int i, int j)
        {
            return i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1);
        }

        static bool CheckInnerCell(char[,] board, int x, int y)
        {
            int counter = 0;
            for (int i = x - 1; i < x + 2; ++i)
            {
                // check three cells above

                if (board[i, y - 1] == '1')
                {
                    counter++;
                }
                //check three cells below
                if (board[i, y + 1] == '1')
                {
                    counter++;
                }
            }
            // check left cell
            if (board[x - 1, y] == '1')
            {
                counter++;
            }
            // check right cell
            if (board[x + 1, y] == '1')
            {
                counter++;
            }
            return IsAlive(board[x, y] == '1', counter);
        }

        static bool CheckBoundaryCell(char[,] board, int x, int y)
        {
            int counter = 0;
            for (int i = x - 1; i < x + 2; ++i)
            {
                // check three cells above
                if (AreIndicesValid(board, i, y - 1) && board[i, y - 1] == '1')
                {
                    counter++;
                }
                //check three cells below
                if (AreIndicesValid(board, i, y + 1) && board[i, y + 1] == '1')
                {
                    counter++;
                }
            }
            // check left cell
            if (AreIndicesValid(board, x - 1, y) && board[x - 1, y] == '1')
            {
                counter++;
            }
            // check right cell
            if (AreIndicesValid(board, x + 1, y) && board[x + 1, y] == '1')
            {
                counter++;
            }
            return IsAlive(board[x, y] == '1', counter);
        }
    }
}
