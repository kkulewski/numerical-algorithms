using System.Text;

namespace Matrix
{
    public static class MyMatrixWriter
    {
        public static string GetFormattedMatrix<T>(MyMatrix<T> matrix) where T : new()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.Rows; i++)
            {
                sb.Append("[");
                for (var j = 0; j < matrix.Cols; j++)
                {
                    sb.Append(matrix[i, j]);
                    if(j < matrix.Cols-1)
                        sb.Append(";");
                }

                sb.Append("]");
                if (i < matrix.Rows - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string GetFormattedVector<T>(T[] vector) where T : new()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (var i = 0; i < vector.Length; i++)
            {
                sb.Append(vector[i]);
                if (i < vector.Length - 1)
                    sb.Append(";");
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
