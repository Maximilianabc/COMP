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

using namespace std;

void runTestCase1();
void runTestCase2();
void runTestCase3();

int main() {
    cout << endl;
    cout << "TEST PROGRAM" << endl;
    cout << "This program is to help you test your code." <<endl;
    cout << "Here are some basic test cases." <<endl;
    cout << "It DOES NOT guarantee the correctness of your code." << endl;
    cout << "To make your game works well, you should at least pass this program without memory leakage" <<endl;
    cout << endl;
    runTestCase1();
    runTestCase2();
    runTestCase3();
    cout << "ALL PASS.:)" << endl;
    cout << "Please further use Dr.Memory to check the memory leakage." <<endl;
    cout << endl;
    return 0;
}

void runTestCase1() {
    // Test Case1
    int steps = 4;
    cout << "->TEST CASE 1: Play with farmland, farmer and employee" << endl;
    // Step 1: assign farmer & employee to farmland
    Farmland* farmland = new Farmland(0, 3);
    Farmer* farmer1 = new Farmer();
    Employee* employee1 = new Employee();
    Employee* employee2 = new Employee();
    Employee* employee3 = new Employee();
    farmland->assignEmployee(farmer1);
    farmland->assignEmployee(employee1);
    farmland->assignEmployee(employee1);
    if (farmland->getNumEmployee() == 2)
        cout<< "->1/"<<steps<<" PASS."<<endl;
    else if (farmland->getNumEmployee() == 3) {
        cout<< "->1/"<<steps<<" FAILED."<<"You might wrongly assign one employee twice."<<endl;
        exit(-1);
    }
    else {
        cout<< "->1/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    // Step2: assign farmer & employee to farmland
    farmland->assignEmployee(employee2);
    farmland->assignEmployee(employee3);
    if (farmland->getNumEmployee() == 3)
        cout<< "->2/"<<steps<<" PASS."<<endl;
    else if (farmland->getNumEmployee() == 4) {
        cout<< "->2/"<<steps<<" FAILED."<<"You might wrongly assign more employees."<<endl;
        exit(-1);
    }
    else {
        cout<< "->2/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }

    // Step3: upgrade farmland test
    farmland->upgrade();
    farmland->assignEmployee(employee3);
    if (farmland->getNumEmployee() == 4)
        cout<< "->3/"<<steps<<" PASS."<<endl;
    else {
        cout<< "->3/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }

    // Step4: fireEmployee test
    farmland->fireEmployee(farmer1);
    const Employee** employee_list;
    farmland->getConstEmployeeList(employee_list);
    int tmp4_f = 0;
    int tmp4_e = 0;
    for (int i = 0; i < farmland->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'r')
            tmp4_f++;
        if(employee_list[i]->getSymbol() == 'e')
            tmp4_e++;
    }
    if (tmp4_f == 0 && tmp4_e == 3)
        cout <<"->4/"<<steps<<" PASS."<<endl;
    else {
        cout<< "->4/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }

    // // Step 5
    // srand(233);
    // int tmp1_5 = farmland->makeMoney();
    //     cout <<"->5/"<<steps<<" "<<tmp1_5<<endl;
    // if (tmp1_5 == 1)
    //     cout <<"->5/"<<steps<<" PASS."<<endl;
    // else {
    //     cout<< "->5/"<<steps<<" FAILED."<<endl;
    //     exit(-1);
    // }
    delete []employee_list;
    delete farmland;
    cout << "->TEST CASE 1: PASS." << endl << endl;
    return;
}

void runTestCase2() {
    int steps = 0;
    cout << "->TEST CASE 2: Play with CattleFarm, Feeder and Cow" << endl;
    steps = 4;
    // Step1: assign feeder & cow to cattlefarm
    Cattlefarm* cattlefarm = new Cattlefarm(0, 3);
    Feeder* feeder = new Feeder();
    Cow* cow = new Cow();
    cattlefarm->assignEmployee(feeder);
    cattlefarm->assignEmployee(cow);
    int tmp2_1_f = 0;
    int tmp2_1_c = 0;
    const Employee** employee_list;
    cattlefarm->getConstEmployeeList(employee_list);
    for (int i = 0; i < cattlefarm->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'd')
            tmp2_1_f++;
        if(employee_list[i]->getSymbol() == 'c')
            tmp2_1_c++;
    }
    delete [] employee_list;
    if (tmp2_1_f == 1 && tmp2_1_c == 1)
        cout <<"->1/"<<steps<<" PASS."<<endl;
    else {
        cout<< "->1/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    // Step2: test isAlive()
    for(int i = 0; i < 10; i++)
        cow->updateWorkAge();
    if (cow->isAlive()) {
        cout<< "->2/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    else{
        cout <<"->2/"<<steps<<" PASS."<<endl;
    }
    // Step3: test removeDiedCow()
    cattlefarm->removeDiedCow();
    int tmp2_3_f = 0;
    int tmp2_3_c = 0;
    cattlefarm->getConstEmployeeList(employee_list);
    for (int i = 0; i < cattlefarm->getNumEmployee(); i++) {
        if(employee_list[i]->getSymbol() == 'd')
            tmp2_3_f++;
        if(employee_list[i]->getSymbol() == 'c')
            tmp2_3_c++;
    }
    if (tmp2_3_f == 1 && tmp2_3_c == 0)
        cout <<"->3/"<<steps<<" PASS."<<endl;
    else {
        cout<< "->3/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    delete []employee_list;
    // Step4: Test assignEmployee after removeDiedCow
    int tmp2_4_fx, tmp2_4_fy;
    int tmp2_4_cx, tmp2_4_cy;
    cattlefarm->getConstEmployeeList(employee_list);
    employee_list[0]->getXY(tmp2_4_fx, tmp2_4_fy);
    Cow* cow1 = new Cow();
    cattlefarm->assignEmployee(cow1);
    cow1->getXY(tmp2_4_cx, tmp2_4_cy);
    if (tmp2_4_cx == 1 && tmp2_4_fx == 1 &&
     (tmp2_4_cy == 4 || tmp2_4_cy == 5) && (tmp2_4_fy == 4 || tmp2_4_fy == 5))
        cout <<"->4/"<<steps<<" PASS."<<endl;
    else {
        cout<< "->4/"<<steps<<" FAILED."
            << "assignEmployee failed after removeDiedCow."
            << "You might wrongly set the X, Y of new employee."<<endl;
        exit(-1);
    }
    delete []employee_list;
    delete cattlefarm;
    cout << "->TEST CASE 2: PASS." << endl << endl;
    return;
}

void runTestCase3()
{
    cout << "->TEST CASE 3: Play with Scene" << endl;
    int steps = 4;
    // Step1: Test add Property and hire
    Scene* s = new Scene();
    int tmp3_init_money = s->getMoney();
    const int tmp3_feeder_cost = 20;
    const int tmp3_cow_cost = 15;
    const int tmp3_cattlefarm_cost = 80;
    const int tmp3_feeder_salary = 5;
    s->addProperty(CATTLEFARM, 0, 3);
    Object* selected_object = s->getObjectAt(0, 3);
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    s->hire(dynamic_cast<Property*>(selected_object), COW);
    if (s->getMoney() == tmp3_init_money - tmp3_feeder_cost - tmp3_cow_cost - tmp3_cattlefarm_cost)
        cout <<"->1/"<<steps<<" PASS."<<endl;
    else {
        cout <<"->1/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    // Step2: Test nextRound
    const int tmp3_intermediate_money = s->getMoney();
    s->nextRound();
    if (s->getMoney() == tmp3_intermediate_money - tmp3_feeder_salary + 10)
        cout <<"->2/"<<steps<<" PASS."<<endl;
    else {
        cout <<"->2/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    // Step3: Test fire
    Employee* wait_for_fire_e;
    int tmp3_3_1, tmp3_3_2;
    wait_for_fire_e = dynamic_cast<Employee*>(s->getObjectAt(1, 4));
    s->fire(wait_for_fire_e);
    tmp3_3_1 = dynamic_cast<Property*>(selected_object)->getNumEmployee();
    s->hire(dynamic_cast<Property*>(selected_object), FEEDER);
    tmp3_3_2 = dynamic_cast<Property*>(selected_object)->getNumEmployee();
    if (tmp3_3_1 == 2 && tmp3_3_2 == 3)
        cout << "->3/" <<steps<<" PASS."<<endl;
    else {
        cout <<"->3/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    // Step4: Test removeProperty
    s->removeProperty(dynamic_cast<Property*>(selected_object));
    int tmp3_4;
    const Object** object_list;
    s->getConstObjects(object_list, tmp3_4);
    if (object_list == nullptr && tmp3_4 == 0)
        cout <<"->4/"<<steps<<" PASS."<<endl;
    else {
        cout <<"->4/"<<steps<<" FAILED."<<endl;
        exit(-1);
    }
    delete s;
    cout << "->TEST CASE 3: PASS." << endl << endl;
    return;
}