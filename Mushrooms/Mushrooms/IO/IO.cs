namespace Mushrooms.IO
{
    public class IO
    {
        public const string FileType = ".txt";
        public const string Matrix = "m" + FileType;
        public const string Vector = "v" + FileType;
        
        public const string PrefixCsharp = "csh_";
        public const string PrefixEigen = "eigen_";

        public const string MonteCarlo = "monte-carlo" + FileType;

        public const string CsharpJacobi = PrefixCsharp + "jacobi" + FileType;
        public const string CsharpGaussSeidel = PrefixCsharp + "gauss-seidel" + FileType;
        public const string CsharpGaussPartialPivot = PrefixCsharp + "gauss-partial" + FileType;
        //public const string CsharpSparseGaussPartialPivot = PrefixCsharp + "sparse-gauss-partial" + FileType;

        public const string EigenGaussPartialPivot = PrefixEigen + "gauss-partial" + FileType;
        //public const string EigenSparseGaussPartialPivot = PrefixEigen + "sparse-gauss-partial" + FileType;

        public const string SummaryType = ".csv";
        public const string SummaryTime = "result_time" + SummaryType;
        public const string SummaryNorm = "result_norm" + SummaryType;
    }
}
