using System;
using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models
{
    public class Board
    {
        public const int Size = 3;

        public Board()
        {
            _data = new PlayerMark?[Size, Size];
        }

        public PlayerMark? this[int row, int column]
        {
            get { return GetMark(row, column); }
            set { SetMark(row, column, value); }
        }

        public IList<Cell> GetEmptyCells()
        {
            var result = new List<Cell>();

            for (int row = 0; row < Size; ++row)
            {
                for (int column = 0; column < Size; ++column)
                {
                    if (!_data[row, column].HasValue)
                    {
                        result.Add(new Cell(row, column));
                    }
                }
            }

            return result;
        }

        public bool IsRowFilledWithMark(int row, PlayerMark mark)
        {
            for (int column = 0; column < Size; ++column)
            {
                if (_data[row, column] != mark)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsColumnFilledWithMark(int column, PlayerMark mark)
        {
            for (int row = 0; row < Size; ++row)
            {
                if (_data[row, column] != mark)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsPrimaryDiagonalFilledWithMark(PlayerMark mark)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (_data[i, i] != mark)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSecondaryDiagonalFilledWithMark(PlayerMark mark)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (_data[i, Size - i - 1] != mark)
                {
                    return false;
                }
            }

            return true;
        }

        public PlayerMark? GetMark(int row, int column)
        {
            if (row < 0 || row >= Size || column < 0 || column >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            return _data[row, column];
        }

        public void SetMark(int row, int column, PlayerMark? mark)
        {
            if (row < 0 || row >= Size || column < 0 || column >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            _data[row, column] = mark;
        }

        private readonly PlayerMark?[,] _data;
    }
}
