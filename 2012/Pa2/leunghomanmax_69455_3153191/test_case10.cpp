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
    // Similar to TestCase2 in test.cpp
    // New a CattleFarm
    Cattlefarm* p = new Cattlefarm(1, 4);
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
    // check assigning
    // Step1: assign feeder & cow to cattlefarm
    Feeder* feeder = new Feeder();
    Cow* cow = new Cow();
    p->assignEmployee(feeder);
    p->assignEmployee(cow);
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
    // Step2: test removeDiedCow()
    for(int i = 0; i < 10; i++) {
        feeder->updateWorkAge();
        cow->updateWorkAge();
    }
    p->removeDiedCow();
    int tmp2_3_f = 0;
    int tmp2_3_c = 0;
    p->getConstEmployeeList(employee_list);
    for (int i = 0; i < p->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'd')
            tmp2_3_f++;
        if(employee_list[i]->getSymbol() == 'c')
            tmp2_3_c++;
    }
    if (tmp2_3_f == 1 && tmp2_3_c == 0)
        output("PASS");
    else
        output("FAILED");
    delete [] employee_list;
    // Step4: Test assignEmployee after removeDiedCow
    int tmp2_4_fx, tmp2_4_fy;
    int tmp2_4_cx, tmp2_4_cy;
    p->getConstEmployeeList(employee_list);
    employee_list[0]->getXY(tmp2_4_fx, tmp2_4_fy);
    Cow* cow1 = new Cow();
    p->assignEmployee(cow1);
    cow1->getXY(tmp2_4_cx, tmp2_4_cy);
    // We declare a Property::isInside function to help grading.
    if (p->isInside(tmp2_4_fx, tmp2_4_fy) && p->isInside(tmp2_4_cx, tmp2_4_cy))
        output("PASS");
    else
        output("FAILED");
    // STEP5: Check make money
    int tmp = p->makeMoney();
    if (tmp == 10 || tmp == 10 - feeder->getSalary())
        output("PASS");
    else
        output("FAILED");
}
