namespace Mushrooms.IO
{
    public class IoConsts
    {
        public const string FileType = ".txt";
        public const string SummaryType = ".csv";

        public const string PrefixInput = "input_";
        public const string PrefixResult = "result_";
        public const string PrefixWinChance = "winchance_";
        public const string PrefixCsharp = "csh_";
        public const string PrefixEigen = "eigen_";

        public const string GameConfig = PrefixInput + "config" + FileType;
        public const string Matrix = PrefixInput + "matrix" + FileType;
        public const string Vector = PrefixInput + "vector" + FileType;
        public const string MatrixGenerationTime = PrefixInput + "time_matrix" + FileType;
        public const string InitialStateIndex = PrefixInput + "initial-state" + FileType;
        
        public const string CsharpGaussSeidel = PrefixResult + PrefixCsharp + "gauss-seidel" + FileType;
        public const string CsharpGaussPartialPivot = PrefixResult + PrefixCsharp + "gauss-partial" + FileType;
        public const string CsharpGaussPartialPivotSparse = PrefixResult + PrefixCsharp + "gauss-partial-sparse" + FileType;
        public const string EigenGaussPartialPivot = PrefixResult + PrefixEigen + "gauss-partial" + FileType;
        public const string EigenGaussPartialPivotSparse = PrefixResult + PrefixEigen + "gauss-partial-sparse" + FileType;

        public const string SummaryTime = "summary_time" + SummaryType;
        public const string SummaryNorm = "summary_norm" + SummaryType;
        public const string SummaryWinChanceTime = "summary_winchance_time" + SummaryType;
        public const string SummaryWinChanceError = "summary_winchance_error" + SummaryType;

        public const string SummaryMethodsTime = "summary" + FileType;
    }
}
