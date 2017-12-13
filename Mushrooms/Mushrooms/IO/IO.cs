namespace Mushrooms.IO
{
    public class IO
    {
        public const string FileType = ".txt";
        public const string Matrix = "m" + FileType;
        public const string Vector = "v" + FileType;
        
        public const string PrefixCsharp = "csh_";
        public const string PrefixEigen = "eigen_";

        public const string CsharpJacobi = PrefixCsharp + "jacobi" + FileType;
        public const string CsharpGaussSeidel = PrefixCsharp + "gauss-seidel" + FileType;
        public const string CsharpGaussPartialPivot = PrefixCsharp + "gauss-partial" + FileType;
    }
}
