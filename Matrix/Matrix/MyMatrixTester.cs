namespace Matrix
{
    public class MyMatrixTester
    {
        public const string FileA = "a.txt";
        public const string FileB = "b.txt";
        public const string FileC = "c.txt";
        public const string FileX = "x.txt";
        public const string PrefixFraction = "fraction_";
        public const string PrefixDouble = "double_";
        public const string PrefixFloat = "float_";

        private readonly int _matrixSize;
        private readonly MyMatrixIoHandler _handler;

        public MyMatrixTester(int matrixSize)
        {
            _matrixSize = matrixSize;
            _handler = new MyMatrixIoHandler();
        }

        public void WriteMatrices()
        {
            // prepare fraction A, B, C matrix + X vector & save
            var frA = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frB = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frC = _handler.GenerateRandomFractionMatrix(_matrixSize);
            var frX = _handler.GenerateRandomFractionVector(_matrixSize);
            _handler.WriteFractionMatrixToFile(frA, PrefixFraction + FileA);
            _handler.WriteFractionMatrixToFile(frB, PrefixFraction + FileB);
            _handler.WriteFractionMatrixToFile(frC, PrefixFraction + FileC);
            _handler.WriteFractionVectorToFile(frX, PrefixFraction + FileX);


            // prepare float A, B, C matrix + X vector & save
            var fA = _handler.FloatMatrixFromFractionMatrix(frA);
            var fB = _handler.FloatMatrixFromFractionMatrix(frB);
            var fC = _handler.FloatMatrixFromFractionMatrix(frC);
            var fX = _handler.FloatVectorFromFractionVector(frX);
            _handler.WriteMatrixToFile(fA, PrefixFloat + FileA);
            _handler.WriteMatrixToFile(fB, PrefixFloat + FileC);
            _handler.WriteMatrixToFile(fC, PrefixFloat + FileB);
            _handler.WriteVectorToFile(fX, PrefixFloat + FileX);

            // prepare double A, B, C matrix + X vector & save
            var dA = _handler.DoubleMatrixFromFractionMatrix(frA);
            var dB = _handler.DoubleMatrixFromFractionMatrix(frB);
            var dC = _handler.DoubleMatrixFromFractionMatrix(frC);
            var dX = _handler.DoubleVectorFromFractionVector(frX);
            _handler.WriteMatrixToFile(dA, PrefixDouble + FileA);
            _handler.WriteMatrixToFile(dB, PrefixDouble + FileB);
            _handler.WriteMatrixToFile(dC, PrefixDouble + FileC);
            _handler.WriteVectorToFile(dX, PrefixDouble + FileX);
        }
    }
}
