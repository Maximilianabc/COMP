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
    // Similar to TestCase1 in test.cpp
    // New a Farmland
    Farmland* p = new Farmland(3, 4);
    output(p->getName());
    output(std::string(1, p->getSymbol()));
    // check x y
    int x, y;
    p->getXY(x, y);
    output(to_string(x));
    output(to_string(y));
    // check size
    p->getSize(x, y);
    output(to_string(x));
    output(to_string(y));
    output(to_string(p->getCost()));
    output(to_string(p->getUpgradeCost()));
    output(to_string(p->getLevel()));
    output(to_string(p->getNumEmployee()));
    output(to_string(p->getMaxNumEmployee()));
    // Step 1: assign farmer & employee to farmland
    Farmer* farmer1 = new Farmer();
    Employee* employee1 = new Employee();
    Employee* employee2 = new Employee();
    Employee* employee3 = new Employee();
    p->assignEmployee(farmer1);
    p->assignEmployee(employee1);
    p->assignEmployee(employee1);
    output(to_string(p->getNumEmployee()));
    // Step2: assign farmer & employee to farmland
    p->assignEmployee(employee2);
    p->assignEmployee(employee3);
    output(to_string(p->getNumEmployee()));
    // Step3: upgrade farmland test
    p->upgrade();
    p->assignEmployee(employee3);
    // Step4: fireEmployee test
    output(to_string(p->getNumEmployee()));
    p->fireEmployee(farmer1);
    const Employee** employee_list;
    p->getConstEmployeeList(employee_list);
    int tmp4_f = 0;
    int tmp4_e = 0;
    for (int i = 0; i < p->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'r')
            tmp4_f++;
        if(employee_list[i]->getSymbol() == 'e')
            tmp4_e++;
    }
    if (tmp4_f == 0 && tmp4_e == 3)
        output("PASS");
    else 
        output("FAILED");
    // Test Destructor
    delete []employee_list;
    delete p;
    // DONE is to test the crash problem
    output("DONE");
}
