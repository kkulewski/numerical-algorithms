#include <fstream>
#include <iostream>
#include <iomanip>
#include "Eigen/Dense"
using namespace Eigen;
using namespace std;


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

void writeToFile(char* fileName, int size, int time, string result)
{
    ofstream file(fileName);
    file << size;
    file << "\n";
    file << time;
    file << "\n";
    file << result;
}

int main()
{
    IOFormat ResultFormat(FullPrecision, DontAlignCols, ";", ";", "", "", "[", "]");

    MatrixXd dA = loadMatrix("double_a.txt");
    MatrixXd dB = loadMatrix("double_b.txt");
    MatrixXd dC = loadMatrix("double_c.txt");
    VectorXd dX = loadVector("double_x.txt");
    int matrixSize = dA.rows();
    VectorXd dPartial(matrixSize);
    VectorXd dFull(matrixSize);

    MatrixXf fA = dA.cast<float>();
    MatrixXf fB = dB.cast<float>();
    MatrixXf fC = dC.cast<float>();
    VectorXf fX = dX.cast<float>();
    VectorXf fPartial(matrixSize);
    VectorXf fFull(matrixSize);


    // PARTIAL PIVOT
    dPartial = dA.partialPivLu().solve(dX);
    fPartial = fA.fullPivLu().solve(fX);
    
    stringstream dPartialResult;   
    stringstream fPartialResult; 

    dPartialResult << dPartial.format(ResultFormat);
    writeToFile("eigen_double_result_full.txt", matrixSize, 0, dPartialResult.str());
    fPartialResult << fPartial.format(ResultFormat);
    writeToFile("eigen_float_result_full.txt", matrixSize, 0, fPartialResult.str());

    // FULL PIVOT
    dFull = dA.fullPivLu().solve(dX);
    fFull = fA.fullPivLu().solve(fX);

    stringstream dFullResult;
    stringstream fFullResult;

    dFullResult << dPartial.format(ResultFormat);
    fFullResult << fPartial.format(ResultFormat);
}