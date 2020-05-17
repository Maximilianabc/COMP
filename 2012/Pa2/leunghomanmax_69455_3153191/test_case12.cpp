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
    // New a CattleFarm, Employee, Farmer, Feeder, Cow
    Cattlefarm* p = new Cattlefarm(2, 2);
    Employee* e = new Employee();
    Farmer* r = new Farmer();
    Feeder* d = new Feeder();
    Cow* c = new Cow();
    // Test whether assign employees to their suitable properties
    p->assignEmployee(e);
    p->assignEmployee(r);
    p->assignEmployee(d);
    p->assignEmployee(c);
    int tmp2_1_f = 0;
    int tmp2_1_c = 0;
    const Employee** employee_list;
    p->getConstEmployeeList(employee_list);
    for (int i = 0; i < p->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'd')
            tmp2_1_f++;
        if(employee_list[i]->getSymbol() == 'c')
            tmp2_1_c++;
    }
    delete [] employee_list;
    if (tmp2_1_f == 1 && tmp2_1_c == 1)
        output("PASS");
    else
        output("FAILED");
    // Test Destructor
    delete p;
    delete e;
    delete r;
    // DONE is to test the crash problem
    output("DONE");
}
