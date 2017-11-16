
#include <fstream>
#include <iostream>
#include "Eigen/Dense"

using namespace Eigen;
using namespace std;

MatrixXd loadMatrix(const char* fileName);
VectorXd loadVector(const char* fileName);
void saveMatrix(char* fileName, int size, int duration, string result);


int main()
{
    IOFormat ResultFormat(FullPrecision, DontAlignCols, " ", " ", "", "", "", "");

    // ****************************************************************************************
    // LOAD MATRICES
    // ****************************************************************************************
    MatrixXd dA = loadMatrix("double_a.txt");
    MatrixXd dB = loadMatrix("double_b.txt");
    MatrixXd dC = loadMatrix("double_c.txt");
    VectorXd dX = loadVector("double_x.txt");
    int matrixSize = dA.rows();

    MatrixXf fA = dA.cast<float>();
    MatrixXf fB = dB.cast<float>();
    MatrixXf fC = dC.cast<float>();
    VectorXf fX = dX.cast<float>();


    // ****************************************************************************************
    // PARTIAL PIVOT
    // ****************************************************************************************
    VectorXd dPartial(matrixSize);
    VectorXf fPartial(matrixSize);

    dPartial = dA.partialPivLu().solve(dX);
    fPartial = fA.partialPivLu().solve(fX);
    
    stringstream dPartialResult;   
    stringstream fPartialResult; 

    dPartialResult << dPartial.format(ResultFormat);
    fPartialResult << fPartial.format(ResultFormat);

    saveMatrix("eigen_double_result_partial.txt", matrixSize, 0, dPartialResult.str());
    saveMatrix("eigen_float_result_partial.txt", matrixSize, 0, fPartialResult.str());


    // ****************************************************************************************
    // FULL PIVOT
    // ****************************************************************************************
    VectorXd dFull(matrixSize);
    VectorXf fFull(matrixSize);

    dFull = dA.fullPivLu().solve(dX);
    fFull = fA.fullPivLu().solve(fX);

    stringstream dFullResult;
    stringstream fFullResult;

    dFullResult << dFull.format(ResultFormat);
    fFullResult << fFull.format(ResultFormat);

    saveMatrix("eigen_double_result_full.txt", matrixSize, 0, dPartialResult.str());
    saveMatrix("eigen_float_result_full.txt", matrixSize, 0, fPartialResult.str());


    // ****************************************************************************************
    // A * X
    // ****************************************************************************************
    MatrixXd dAX(matrixSize, matrixSize);
    MatrixXf fAX(matrixSize, matrixSize);

    dAX = dA * dX;
    fAX = fA * fX;

    stringstream dAXResult;
    stringstream fAXResult;

    dAXResult << dAX.format(ResultFormat);
    fAXResult << fAX.format(ResultFormat);
    
    saveMatrix("eigen_double_result_ax.txt", matrixSize, 0, dAXResult.str());
    saveMatrix("eigen_float_result_ax.txt", matrixSize, 0, fAXResult.str());


    // ****************************************************************************************
    // (A + B + C) * X
    // ****************************************************************************************
    MatrixXd dABCX(matrixSize, matrixSize);
    MatrixXf fABCX(matrixSize, matrixSize);

    dABCX = (dA + dB + dC) * dX;
    fABCX = (fA + fB + fC) * fX;

    stringstream dABCXResult;
    stringstream fABCXResult;

    dABCXResult << dABCX.format(ResultFormat);
    fABCXResult << fABCX.format(ResultFormat);

    saveMatrix("eigen_double_result_abcx.txt", matrixSize, 0, dABCXResult.str());
    saveMatrix("eigen_float_result_abcx.txt", matrixSize, 0, fABCXResult.str());


    // ****************************************************************************************
    // A * (B * C)
    // ****************************************************************************************
    MatrixXd dABC(matrixSize, matrixSize);
    MatrixXf fABC(matrixSize, matrixSize);

    dABC = dA * (dB * dC);
    fABC = fA * (fB * fC);

    stringstream dABCResult;
    stringstream fABCResult;

    dABCResult << dABC.format(ResultFormat);
    fABCResult << fABC.format(ResultFormat);

    saveMatrix("eigen_double_result_abc.txt", matrixSize, 0, dABCResult.str());
    saveMatrix("eigen_float_result_abc.txt", matrixSize, 0, fABCResult.str());

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


void saveMatrix(char* fileName, int size, int duration, string result)
{
    ofstream file(fileName);
    file << size;
    file << "\n";
    file << duration;
    file << "\n";
    file << result;
}