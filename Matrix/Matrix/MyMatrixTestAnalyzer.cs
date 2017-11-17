using System;

namespace Matrix
{
    class MyMatrixTestAnalyzer
    {
        private MyMatrixIoHandler _handler = new MyMatrixIoHandler();

        public void CompareGaussNoPivot()
        {
            var fractionTime = _handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultNoPivot, true).Item2;
            var doubleTime = _handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultNoPivot, true).Item2;
            var floatTime = _handler.LoadFloatVector(IO.PrefixFloat + IO.ResultNoPivot, true).Item2;

            Console.WriteLine("fr: {0}\ndo: {1}\nfl: {2}", 
                fractionTime, doubleTime, floatTime);
        }

        public void CompareGaussPartialPivot()
        {
            var handler = new MyMatrixIoHandler();

            var fractionTime = handler.LoadDoubleVector(IO.PrefixFraction + IO.ResultPartialPivot, true).Item2;
            var doubleTime = handler.LoadDoubleVector(IO.PrefixDouble + IO.ResultPartialPivot, true).Item2;
            var floatTime = handler.LoadFloatVector(IO.PrefixFloat + IO.ResultPartialPivot, true).Item2;
            var doubleTimeEigen = handler.LoadDoubleVector(IO.PrefixEigen + IO.PrefixDouble + IO.ResultPartialPivot, true).Item2;
            var floatTimeEigen = handler.LoadFloatVector(IO.PrefixEigen + IO.PrefixFloat + IO.ResultPartialPivot, true).Item2;

             Console.WriteLine("fr: {0}\ndo: {1}\nfl: {2}\ned: {3}\nef: {4}", 
                fractionTime, doubleTime, floatTime, 
               doubleTimeEigen, floatTimeEigen);
        }
    }
}
