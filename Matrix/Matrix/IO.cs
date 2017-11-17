namespace Matrix
{
    public class IO
    {
        public const string FileType = ".txt";

        public const string PrefixFraction = "fraction_";
        public const string PrefixDouble = "double_";
        public const string PrefixFloat = "float_";

        public const string FileA = "a" + FileType;
        public const string FileB = "b" + FileType;
        public const string FileC = "c" + FileType;
        public const string FileX = "x" + FileType;

        public const string PrefixResult = "result_";
        public const string PrefixEigen = "eigen_";

        public const string ResultAx = PrefixResult + "ax" + FileType;
        public const string ResultAbcx = PrefixResult + "abcx" + FileType;
        public const string ResultAbc = PrefixResult + "abc" + FileType;

        public const string ResultNoPivot = PrefixResult + "no" + FileType;
        public const string ResultPartialPivot = PrefixResult + "partial" + FileType;
        public const string ResultFullPivot = PrefixResult + "full" + FileType;

    }
}
