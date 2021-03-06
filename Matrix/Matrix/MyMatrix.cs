﻿using System;

namespace Matrix
{
    public class MyMatrix<T> where T: new()
    {
        public T[,] Matrix { get; }

        public MyMatrix(T[,] matrix)
        {
            Matrix = matrix;
        }

        public MyMatrix(int rows, int cols)
        {
            var random = new Random();
            var matrix = new T[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (matrix is int[,])
                    {
                        matrix[i, j] = (dynamic)random.Next();
                    }
                    else if (matrix is double[,])
                    {
                        matrix[i, j] = (dynamic) random.NextDouble();
                    }
                    else if (matrix is float[,])
                    {
                        matrix[i, j] = (dynamic) (float) random.NextDouble();
                    }
                    else if (matrix is Fraction[,])
                    {
                        matrix[i, j] = (dynamic) new Fraction(random.Next(), random.Next(1, int.MaxValue));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid type!");
                    }
                }
            }

            Matrix = matrix;
        }

        public int Rows => Matrix.GetLength(0);

        public int Cols => Matrix.GetLength(1);

        public T this[int row, int col]
        {
            get => Matrix[row, col];
            set => Matrix[row, col] = value;
        }

        public static MyMatrix<T> operator +(MyMatrix<T> a, MyMatrix<T> b)
        {
            if(a.Rows != b.Rows || a.Cols != b.Cols)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows, a.Cols];
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Cols; j++)
                {
                    output[i, j] = (dynamic)a[i, j] + (dynamic)b[i, j];
                }
            }

            return new MyMatrix<T>(output);
        }

        public static MyMatrix<T> operator *(MyMatrix<T> a, MyMatrix<T> b)
        {
            if (a.Cols != b.Rows)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows, b.Cols];
            for (var row = 0; row < a.Rows; row++)
            {
                for (var col = 0; col < b.Cols; col++)
                {
                    var sum = new T();
                    for (var k = 0; k < a.Cols; k++)
                    {
                        sum += (dynamic) a[row, k] * (dynamic) b[k, col];
                    }
                    
                    output[row, col] = sum;
                }
            }

            return new MyMatrix<T>(output);
        }

        public static T[] operator *(MyMatrix<T> a, T[] b)
        {
            if (a.Cols != b.Length)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows];
            for (var row = 0; row < a.Rows; row++)
                output[row] = new T();

            for (var row = 0; row < a.Rows; row++)
            {
                for (var col = 0; col < b.Length; col++)
                {
                    output[row] += (dynamic) a[row, col] * (dynamic) b[col];
                }
            }

            return output;
        }

        public void SwapRow(int index1, int index2)
        {
            for (var i = 0; i < Cols; i++)
            {
                var temp = this[index2, i];
                this[index2, i] = this[index1, i];
                this[index1, i] = temp;
            }
        }

        public void SwapColumn(int index1, int index2)
        {
            for (var i = 0; i < Cols; i++)
            {
                var temp = this[i, index2];
                this[i, index2] = this[i, index1];
                this[i, index1] = temp;
            }
        }

        public int FindMaxInColumn(int selected)
        {
            // set selected row as current max
            var currentMaxRowIndex = selected;
            var currentMax = this[selected, selected];

            // check each row below selected row
            for(var i = selected; i < Rows; i++)
            {
                if (this[i, selected] > (dynamic) currentMax)
                {
                    currentMax = this[i, selected];
                    currentMaxRowIndex = i;
                }
            }

            return currentMaxRowIndex;
        }

        public Tuple<int, int> FindMax(int selected)
        {
            var currentMaxIndex = new Tuple<int, int>(selected, selected);
            var currentMax = this[selected, selected];

            for (var i = selected; i < Rows; i++)
            {
                for (var j = selected; j < Cols; j++)
                {
                    if (this[i, j] > (dynamic) currentMax || this[i, j] < -(dynamic)currentMax)
                    {
                        currentMax = this[i, j];
                        currentMaxIndex = new Tuple<int, int>(i, j);
                    }
                }
            }

            return currentMaxIndex;
        }
        
        public void GaussianReductionNoPivot(T[] vector)
        {
            ReduceLeftBottomTriangle(vector);
            ReduceRightTopTriangle(vector);
            ToIdentityMatrix(vector);
        }

        public void GaussianReductionPartialPivot(T[] vector)
        {
            ReduceLeftBottomTrianglePartialPivot(vector);
            ReduceRightTopTriangle(vector);
            ToIdentityMatrix(vector);
        }

        public void GaussianReductionFullPivot(T[] vector)
        {
            // initial column order
            var columnOrder = new int[Cols];
            for (var i = 0; i < Cols; i++)
                columnOrder[i] = i;

            ReduceLeftBottomTriangleFullPivot(vector, columnOrder);
            ReduceRightTopTriangle(vector);
            ToIdentityMatrix(vector);

            // reorder colums back
            var orderedVector = new T[Cols];
            for (var i = 0; i < Cols; i++)
                orderedVector[columnOrder[i]] = vector[i];

            for (var i = 0; i < Cols; i++)
                vector[i] = orderedVector[i];
        }

        public void ReduceLeftBottomTriangle(T[] vector)
        {
            // select row that will be used to reduce rows below it
            for (var selected = 0; selected < Rows - 1; selected++)
            {
                EnsureNoLeadingZero(selected);

                // loop on each row below selected row
                for (var current = selected + 1; current < Rows; current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void ReduceLeftBottomTrianglePartialPivot(T[] vector)
        {
            // select row that will be used to reduce rows below it
            for (var selected = 0; selected < Rows - 1; selected++)
            {
                EnsureNoLeadingZero(selected);
                ChoosePartialPivot(vector, selected);

                // loop on each row below selected row
                for (var current = selected + 1; current < Rows; current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void ChoosePartialPivot(T[] vector, int selected)
        {
            var maxRow = FindMaxInColumn(selected);
            // if its not the biggest element in current column
            if (selected != maxRow)
            {
                // swap vector rows
                var temp = vector[selected];
                vector[selected] = vector[maxRow];
                vector[maxRow] = temp;

                // swap matrix rows
                SwapRow(selected, maxRow);
            }
        }
        
        public void ReduceLeftBottomTriangleFullPivot(T[] vector, int[] columnOrder)
        {
            // select row that will be used to reduce rows below it
            for (var selected = 0; selected < Rows - 1; selected++)
            {
                var max = FindMax(selected);
                // swap columns
                var tempOrd = columnOrder[selected];
                columnOrder[selected] = columnOrder[max.Item2];
                columnOrder[max.Item2] = tempOrd;
                SwapColumn(selected, max.Item2);

                // swap row + vector
                var temp = vector[selected];
                vector[selected] = vector[max.Item1];
                vector[max.Item1] = temp;
                SwapRow(selected, max.Item1);

                // loop on each row below selected row
                for (var current = selected + 1; current < Rows; current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void ReduceRightTopTriangle(T[] vector)
        {
            // select last row that will be used to reduce rows above it
            for (var selected = Rows - 1; selected >= 1; selected--)
            {
                EnsureNoLeadingZero(selected);

                // loop on each row above selected row
                for (var current = selected - 1; current >= 0; current--)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        private void EnsureNoLeadingZero(int selected)
        {
            if (this[selected, selected] == (dynamic)new T())
                throw new ArgumentException("Matrix diagonal contains zero! (leading zero detected)");
        }

        private void ReduceRow(T[] vector, int selected, int current)
        {
            // if current row is already reduced (leading 0) => return
            if (this[current, selected] == (dynamic) new T())
                return;

            // get scalar for current row
            var scalar = this[current, selected] / (dynamic) this[selected, selected];

            // substract selected row (multiplied by scalar) from current row
            for (var col = 0; col < Cols; col++)
            {
                // substract each column
                this[current, col] -= this[selected, col] * scalar;
            }

            // substract selected vector row (multiplied by scalar) from current row
            vector[current] -= vector[selected] * scalar;
        }

        public void ToIdentityMatrix(T[] v)
        {
            for (var i = 0; i < Rows; i++)
            {
                v[i] = v[i] / (dynamic) this[i, i];
                this[i, i] = this[i, i] / (dynamic) this[i, i];
            }
        }
    }
}
