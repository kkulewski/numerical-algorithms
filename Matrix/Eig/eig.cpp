
#include <chrono>
#include <fstream>
#include <iostream>
#include "Eigen/Dense"

using namespace Eigen;
using namespace std;
using namespace chrono;

MatrixXd loadMatrix(const char* fileName);
VectorXd loadVector(const char* fileName);
void saveMatrix(char* fileName, int size, long long durationNs, string result);


int main(int argc, char* argv[])
{
    int testCountArg = atoi(argv[1]);
    int testCount = testCountArg > 0 ? testCountArg : 1;

    IOFormat ResultFormat(FullPrecision, DontAlignCols, " ", " ", "", "", "", "");
    auto start = high_resolution_clock::now();
    auto end = high_resolution_clock::now();

    // ****************************************************************************************
    // LOAD MATRICES
    // ****************************************************************************************
    MatrixXd dA = loadMatrix("double_a.txt");
    MatrixXd dB = loadMatrix("double_b.txt");
    MatrixXd dC = loadMatrix("double_c.txt");
    VectorXd dX = loadVector("double_x.txt");

    MatrixXf fA = dA.cast<float>();
    MatrixXf fB = dB.cast<float>();
    MatrixXf fC = dC.cast<float>();
    VectorXf fX = dX.cast<float>();

    int matrixSize = dA.rows();

    // ****************************************************************************************
    // PARTIAL PIVOT
    // ****************************************************************************************
    VectorXd dPartial(matrixSize);
    VectorXf fPartial(matrixSize);

    long long dPartialNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dPartial = dA.partialPivLu().solve(dX);
        end = high_resolution_clock::now();
        dPartialNs += duration_cast<nanoseconds>(end - start).count();
    }
    
    long long fPartialNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        fPartial = fA.partialPivLu().solve(fX);
        end = high_resolution_clock::now();
        fPartialNs += duration_cast<nanoseconds>(end - start).count();
    }
    
    stringstream dPartialResult;   
    stringstream fPartialResult; 

    dPartialResult << dPartial.format(ResultFormat);
    fPartialResult << fPartial.format(ResultFormat);

    saveMatrix("eigen_double_result_partial.txt", matrixSize, dPartialNs / testCount, dPartialResult.str());
    saveMatrix("eigen_float_result_partial.txt", matrixSize, fPartialNs / testCount, fPartialResult.str());


    // ****************************************************************************************
    // FULL PIVOT
    // ****************************************************************************************
    VectorXd dFull(matrixSize);
    VectorXf fFull(matrixSize);

    long long dFullNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dFull = dA.fullPivLu().solve(dX);
        end = high_resolution_clock::now();
        dFullNs += duration_cast<nanoseconds>(end - start).count();
    }

    long long fFullNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        fFull = fA.fullPivLu().solve(fX);
        end = high_resolution_clock::now();
        fFullNs += duration_cast<nanoseconds>(end - start).count();
    }

    stringstream dFullResult;
    stringstream fFullResult;

    dFullResult << dFull.format(ResultFormat);
    fFullResult << fFull.format(ResultFormat);

    saveMatrix("eigen_double_result_full.txt", matrixSize, dFullNs / testCount, dPartialResult.str());
    saveMatrix("eigen_float_result_full.txt", matrixSize, fFullNs / testCount, fPartialResult.str());


    // ****************************************************************************************
    // A * X
    // ****************************************************************************************
    MatrixXd dAX(matrixSize, matrixSize);
    MatrixXf fAX(matrixSize, matrixSize);

    long long dAXNs = 0;
    int di = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dAX = dA * dX;
        end = high_resolution_clock::now();
        dAXNs += duration_cast<nanoseconds>(end - start).count();
    }

    long long fAXNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        fAX = fA * fX;
        end = high_resolution_clock::now();
        fAXNs += duration_cast<nanoseconds>(end - start).count();
    }

    stringstream dAXResult;
    stringstream fAXResult;

    dAXResult << dAX.format(ResultFormat);
    fAXResult << fAX.format(ResultFormat);
    
    saveMatrix("eigen_double_result_ax.txt", matrixSize, dAXNs / testCount, dAXResult.str());
    saveMatrix("eigen_float_result_ax.txt", matrixSize, fAXNs / testCount, fAXResult.str());


    // ****************************************************************************************
    // (A + B + C) * X
    // ****************************************************************************************
    MatrixXd dABCX(matrixSize, matrixSize);
    MatrixXf fABCX(matrixSize, matrixSize);

    long long dABCXNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dABCX = (dA + dB + dC) * dX;
        end = high_resolution_clock::now();
        dABCXNs += duration_cast<nanoseconds>(end - start).count();
    }

    long long fABCXNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        fABCX = (fA + fB + fC) * fX;
        end = high_resolution_clock::now();
        fABCXNs += duration_cast<nanoseconds>(end - start).count();
    }

    stringstream dABCXResult;
    stringstream fABCXResult;

    dABCXResult << dABCX.format(ResultFormat);
    fABCXResult << fABCX.format(ResultFormat);

    saveMatrix("eigen_double_result_abcx.txt", matrixSize, dABCXNs / testCount, dABCXResult.str());
    saveMatrix("eigen_float_result_abcx.txt", matrixSize, fABCXNs / testCount, fABCXResult.str());


    // ****************************************************************************************
    // A * (B * C)
    // ****************************************************************************************
    MatrixXd dABC(matrixSize, matrixSize);
    MatrixXf fABC(matrixSize, matrixSize);

    long long dABCNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dABC = dA * (dB * dC);
        end = high_resolution_clock::now();
        dABCNs += duration_cast<nanoseconds>(end - start).count();
    }

    long long fABCNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        fABC = fA * (fB * fC);
        end = high_resolution_clock::now();
        fABCNs += duration_cast<nanoseconds>(end - start).count();
    }

    stringstream dABCResult;
    stringstream fABCResult;

    dABCResult << dABC.format(ResultFormat);
    fABCResult << fABC.format(ResultFormat);

    saveMatrix("eigen_double_result_abc.txt", matrixSize, dABCNs / testCount, dABCResult.str());
    saveMatrix("eigen_float_result_abc.txt", matrixSize, fABCNs / testCount, fABCResult.str());

    return 0;
}


// ****************************************************************************************
// IO operations
// ****************************************************************************************

MatrixXd loadMatrix(const char* fileName)
{
    ifstream file(fileName);
    int size;
    file >> size;

    MatrixXd matrix(size, size);
    for(int i = 0; i < size; i++)
    {
        for(int j = 0; j < size; j++)
        {
            file >> matrix(i, j);
        }
    }

    return matrix;
}


VectorXd loadVector(const char* fileName)
{
    ifstream file(fileName);
    int size;
    file >> size;

    VectorXd vector(size);
    for(int i = 0; i < size; i++)
    {
        file >> vector(i);
    }

    return vector;
}


void saveMatrix(char* fileName, int size, long long durationNs, string result)
{
    ofstream file(fileName);
    file << durationNs / (1000.0 * 1000.0);
    file << "\n";
    file << size;
    file << "\n";
    file << result;
}