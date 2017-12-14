
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

    IOFormat VResultFormat(FullPrecision, DontAlignCols, " ", " ", "", "", "", "");
    IOFormat MResultFormat(FullPrecision, DontAlignCols, " ", "", "", "\n", "", "");
    auto start = high_resolution_clock::now();
    auto end = high_resolution_clock::now();

    
    // LOAD MATRICES
    MatrixXd dA = loadMatrix("input_matrix.txt");
    VectorXd dX = loadVector("input_vector.txt");
    int matrixSize = dA.rows();


    // PARTIAL PIVOT
    VectorXd dPartial(matrixSize);

    long long dPartialNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();
        dPartial = dA.partialPivLu().solve(dX);
        end = high_resolution_clock::now();
        dPartialNs += duration_cast<nanoseconds>(end - start).count();
    }
    
    stringstream dPartialResult;
    dPartialResult << dPartial.format(VResultFormat);
    saveMatrix("eigen_gauss-partial.txt", matrixSize, dPartialNs / testCount, dPartialResult.str());


    // SPARSE LU
}

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