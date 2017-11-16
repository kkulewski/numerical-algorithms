using System.Text;

namespace Matrix
{
    public static class MyMatrixFormatter
    {
        public static string GetFormattedMatrix<T>(MyMatrix<T> matrix) where T : new()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Cols; j++)
                {
                    sb.Append(matrix[i, j]);
                    if(j < matrix.Cols-1)
                        sb.Append(" ");
                }
                
                if (i < matrix.Rows - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string GetFormattedVector<T>(T[] vector) where T : new()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < vector.Length; i++)
            {
                sb.Append(vector[i]);
                if (i < vector.Length - 1)
                    sb.Append(" ");
            }
            
            return sb.ToString();
        }

        public static string GetFormattedFractionMatrix(MyMatrix<Fraction> matrix)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Cols; j++)
                {
                    sb.Append(matrix[i, j].Numerator);
                    sb.Append("/");
                    sb.Append(matrix[i, j].Denominator);
                    if (j < matrix.Cols - 1)
                        sb.Append(" ");
                }
                
                if (i < matrix.Rows - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string GetFormattedFractionVector(Fraction[] vector)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < vector.Length; i++)
            {
                sb.Append(vector[i].Numerator);
                sb.Append("/");
                sb.Append(vector[i].Denominator);
                if (i < vector.Length - 1)
                    sb.Append(" ");
            }
            
            return sb.ToString();
        }
    }
}
