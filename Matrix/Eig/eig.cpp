
#include <chrono>
#include <fstream>
#include <iostream>
#include "Eigen/Dense"

using namespace Eigen;
using namespace std;
using namespace chrono;

MatrixXd loadMatrix(const char* fileName);
VectorXd loadVector(const char* fileName);
void saveMatrix(char* fileName, int size, double durationUs, string result);


int main()
{
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

    start = high_resolution_clock::now();
    dPartial = dA.partialPivLu().solve(dX);
    end = high_resolution_clock::now();
    auto dPartialMs = duration_cast<microseconds>(end - start).count();
    
    start = high_resolution_clock::now();
    fPartial = fA.partialPivLu().solve(fX);
    end = high_resolution_clock::now();
    auto fPartialMs = duration_cast<microseconds>(end - start).count();
    
    stringstream dPartialResult;   
    stringstream fPartialResult; 

    dPartialResult << dPartial.format(ResultFormat);
    fPartialResult << fPartial.format(ResultFormat);

    saveMatrix("eigen_double_result_partial.txt", matrixSize, dPartialMs, dPartialResult.str());
    saveMatrix("eigen_float_result_partial.txt", matrixSize, fPartialMs, fPartialResult.str());


    // ****************************************************************************************
    // FULL PIVOT
    // ****************************************************************************************
    VectorXd dFull(matrixSize);
    VectorXf fFull(matrixSize);

    start = high_resolution_clock::now();
    dFull = dA.fullPivLu().solve(dX);
    end = high_resolution_clock::now();
    auto dFullMs = duration_cast<microseconds>(end - start).count();

    start = high_resolution_clock::now();
    fFull = fA.fullPivLu().solve(fX);
    end = high_resolution_clock::now();
    auto fFullMs = duration_cast<microseconds>(end - start).count();

    stringstream dFullResult;
    stringstream fFullResult;

    dFullResult << dFull.format(ResultFormat);
    fFullResult << fFull.format(ResultFormat);

    saveMatrix("eigen_double_result_full.txt", matrixSize, dFullMs, dPartialResult.str());
    saveMatrix("eigen_float_result_full.txt", matrixSize, fFullMs, fPartialResult.str());


    // ****************************************************************************************
    // A * X
    // ****************************************************************************************
    MatrixXd dAX(matrixSize, matrixSize);
    MatrixXf fAX(matrixSize, matrixSize);

    start = high_resolution_clock::now();
    for(int i = 0; i < 10; i++)
        dAX = dA * dX;
    end = high_resolution_clock::now();
    auto dAXMs = duration_cast<microseconds>(end - start).count();

    start = high_resolution_clock::now();
    for(int i = 0; i < 10; i++)
        fAX = fA * fX;
    end = high_resolution_clock::now();
    auto fAXMs = duration_cast<microseconds>(end - start).count();

    stringstream dAXResult;
    stringstream fAXResult;

    dAXResult << dAX.format(ResultFormat);
    fAXResult << fAX.format(ResultFormat);
    
    saveMatrix("eigen_double_result_ax.txt", matrixSize, dAXMs / 10.0, dAXResult.str());
    saveMatrix("eigen_float_result_ax.txt", matrixSize, fAXMs / 10.0, fAXResult.str());


    // ****************************************************************************************
    // (A + B + C) * X
    // ****************************************************************************************
    MatrixXd dABCX(matrixSize, matrixSize);
    MatrixXf fABCX(matrixSize, matrixSize);

    start = high_resolution_clock::now();
    dABCX = (dA + dB + dC) * dX;
    end = high_resolution_clock::now();
    auto dABCXMs = duration_cast<microseconds>(end - start).count();

    start = high_resolution_clock::now();
    fABCX = (fA + fB + fC) * fX;
    end = high_resolution_clock::now();
    auto fABCXMs = duration_cast<microseconds>(end - start).count();

    stringstream dABCXResult;
    stringstream fABCXResult;

    dABCXResult << dABCX.format(ResultFormat);
    fABCXResult << fABCX.format(ResultFormat);

    saveMatrix("eigen_double_result_abcx.txt", matrixSize, dABCXMs, dABCXResult.str());
    saveMatrix("eigen_float_result_abcx.txt", matrixSize, fABCXMs, fABCXResult.str());


    // ****************************************************************************************
    // A * (B * C)
    // ****************************************************************************************
    MatrixXd dABC(matrixSize, matrixSize);
    MatrixXf fABC(matrixSize, matrixSize);

    start = high_resolution_clock::now();
    dABC = dA * (dB * dC);
    end = high_resolution_clock::now();
    auto dABCMs = duration_cast<microseconds>(end - start).count();

    start = high_resolution_clock::now();
    fABC = fA * (fB * fC);
    end = high_resolution_clock::now();
    auto fABCMs = duration_cast<microseconds>(end - start).count();

    stringstream dABCResult;
    stringstream fABCResult;

    dABCResult << dABC.format(ResultFormat);
    fABCResult << fABC.format(ResultFormat);

    saveMatrix("eigen_double_result_abc.txt", matrixSize, dABCMs, dABCResult.str());
    saveMatrix("eigen_float_result_abc.txt", matrixSize, fABCMs, fABCResult.str());

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


void saveMatrix(char* fileName, int size, double durationUs, string result)
{
    ofstream file(fileName);
    file << durationUs / 1000.0;
    file << "\n";
    file << size;
    file << "\n";
    file << result;
}