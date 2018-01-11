
#include <chrono>
#include <fstream>
#include <iostream>
#include "Eigen/Dense"
#include "Eigen/Sparse"
#include "Eigen/SparseLU"

#define INPUT_MATRIX "input_matrix.txt"
#define INPUT_VECTOR "input_vector.txt"
#define INPUT_INITIAL_STATE "input_initial-state.txt"
#define WINCHANCE_GAUSS_PARTIAL "result_eigen_gauss-partial.txt"
#define WINCHANCE_SPARSE "result_eigen_gauss-partial-sparse.txt"

using namespace Eigen;
using namespace std;
using namespace chrono;

MatrixXd loadMatrix(const char* fileName);
VectorXd loadVector(const char* fileName);
int loadInitialStateIndex(const char* fileName);
void saveMatrix(char* fileName, int size, long long durationNs, string result);


int main(int argc, char* argv[])
{
    int testCountArg = atoi(argv[1]);
    int testCount = testCountArg > 0 ? testCountArg : 10;

    IOFormat VResultFormat(FullPrecision, DontAlignCols, " ", " ", "", "", "", "");
    IOFormat MResultFormat(FullPrecision, DontAlignCols, " ", "", "", "\n", "", "");
    auto start = high_resolution_clock::now();
    auto end = high_resolution_clock::now();

    
    // LOAD MATRICES
    MatrixXd dA = loadMatrix(INPUT_MATRIX);
    VectorXd dX = loadVector(INPUT_VECTOR);
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

    // PARTIAL PIVOT - WIN CHANCE
    VectorXd dPartialWinChance(1);
    dPartialWinChance(0) = dPartial(loadInitialStateIndex(INPUT_INITIAL_STATE)); 
    stringstream dPartialWinChanceResult;
    dPartialWinChanceResult << dPartialWinChance.format(VResultFormat);
    saveMatrix(WINCHANCE_GAUSS_PARTIAL, 1, dPartialNs / testCount, dPartialWinChanceResult.str());


    // SPARSE LU
    SparseMatrix<double> sdA = dA.sparseView();
    SparseVector<double> sdX = dX.sparseView();

    SparseLU<Eigen::SparseMatrix<double>> solver;
    sdA.makeCompressed();
    solver.analyzePattern(sdA);
    solver.factorize(sdA);

    VectorXd dSparse;

    long long dSparseNs = 0;
    for (int i = 0; i < testCount; i++)
    {
        start = high_resolution_clock::now();  
        dSparse = solver.solve(sdX);
        end = high_resolution_clock::now();
        dSparseNs += duration_cast<nanoseconds>(end - start).count();
    }

    // SPARSE - WIN CHANCE
    VectorXd dSparseWinChance(1);
    dSparseWinChance(0) = dSparse(loadInitialStateIndex(INPUT_INITIAL_STATE)); 
    stringstream dSparseWinChanceResult;
    dSparseWinChanceResult << dSparseWinChance.format(VResultFormat);
    saveMatrix(WINCHANCE_SPARSE, 1, dSparseNs / testCount, dSparseWinChanceResult.str());
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

int loadInitialStateIndex(const char* fileName)
{
    ifstream file(fileName);
    int index;
    file >> index;

    return index;
}