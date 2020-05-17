//this is one of the two files that you need to submit

#include "infectionAnalyzer.h"
#include <fstream> //for reading file
#include <queue> //you may use STL queue
#include <algorithm> //you may use STL algorithm
using namespace std;
//you are NOT allowed to include additional libraries yourself

InfectionAnalyzer::~InfectionAnalyzer()
{
}

bool InfectionAnalyzer::loadInfectionFile(const string filename)
{
    return false;
}

bool InfectionAnalyzer::isInfected(const string name) const
{
    return false;
}

string InfectionAnalyzer::getInfectionSource(const string name) const
{
    return string();
}

int InfectionAnalyzer::getInfectionGeneration(const string name) const
{
    return 0;
}

const vector<pair<string, int>>& InfectionAnalyzer::getInfectionPowerVector()
{
    // TODO: insert return statement here
}
