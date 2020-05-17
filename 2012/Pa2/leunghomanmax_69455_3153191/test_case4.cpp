/*
 * File Created: Mon Mar 23 2020
 * Author: Peng YUN (pyun@ust.hk)
 * You can change this file to add more test cases.
*/

#include "Employee.h"
#include "TodoFarmer.h"
#include "TodoFeeder.h"
#include "TodoCow.h"
#include "Property.h"
#include "TodoFarmland.h"
#include "TodoCattlefarm.h"
#include "Scene.h"
#include <string>
#include <sstream>
#include <fstream>

using namespace std;

std::string output_path = "";

void runTestCase();
void output(string s) {
    static int conter = 0;
    ofstream f(output_path, std::ios_base::app);
    f << ++conter << " : " << s << endl;
    f.close();
};

int main(int argc, char *argv[]) {
    output_path = argv[1];
    runTestCase();
    return 0;
}

void runTestCase() {
    // TEST without testing destructor
    // New a Feeder
    Feeder* e = new Feeder();
    // Once constructed, the states of all types of employees are set as NORMAL.
    if (e->getState() == ObjectState::NORMAL)
        output("PASS");
    // Here we only call these two functions to check whether you implement them or not.
    // The correctness testing is left at the further TestCases.
    for(int i = 0; i < 10; i++) {
        e->updateState();
        e->updateWorkAge();
    }
    output(to_string(e->getCost()));
    output(to_string(e->getNumWorkDays()));
    output(to_string(e->getNumRestDays()));
    output(to_string(e->getSalary()));
    output(e->getName());
    output(std::string(1, e->getSymbol()));
}
