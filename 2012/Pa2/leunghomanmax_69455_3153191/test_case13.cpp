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
    // Similar to TestCase3 in test.cpp
    // New a Scene
    // Step1: Test add Property and hire
    Scene* s = new Scene();
    int tmp3_init_money = s->getMoney();
    const int tmp3_feeder_cost = 20;
    const int tmp3_cow_cost = 15;
    const int tmp3_cattlefarm_cost = 80;
    const int tmp3_feeder_salary = 5;
    s->addProperty(CATTLEFARM, 1, 4);
    Object* selected_object = s->getObjectAt(1, 4);
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    s->hire(dynamic_cast<Property*>(selected_object), COW);
    if (s->getMoney() == tmp3_init_money - tmp3_feeder_cost - tmp3_cow_cost - tmp3_cattlefarm_cost)
        output("PASS");
    else
        output("FAILED");
     const int tmp3_intermediate_money = s->getMoney();
     // Step2: Test nextRound
    s->nextRound();
    if (s->getMoney() == tmp3_intermediate_money - tmp3_feeder_salary + 10)
        output("PASS");
    else
        output("FAILED");
    // Step3: Test fire
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    Employee* wait_for_fire_e;
    int tmp3_3_1, tmp3_3_2;
    wait_for_fire_e = dynamic_cast<Employee*>(s->getObjectAt(2, 5));
    s->fire(wait_for_fire_e);
    tmp3_3_1 = dynamic_cast<Property*>(selected_object)->getNumEmployee();
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    tmp3_3_2 = dynamic_cast<Property*>(selected_object)->getNumEmployee();
    if (tmp3_3_1 == 2 && tmp3_3_2 == 3)
        output("PASS");
    else
        output("FAILED");
    // Step4: Test removeProperty
    s->removeProperty(dynamic_cast<Property*>(selected_object));
    int tmp3_4;
    const Object** object_list;
    s->getConstObjects(object_list, tmp3_4);
    if (object_list == nullptr && tmp3_4 == 0)
        output("PASS");
    else 
        output("FAILED");
    delete s;
    // DONE is to test the crash problem
    output("DONE");
}
