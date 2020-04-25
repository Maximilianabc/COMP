/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "TodoFarmer.h"

// Farmer cost 10 salary 2.
// Farmer 3 days work & 1 day rest.
const int FARMER_COST = 10;
const int FARMER_SALARY = 2;
const int FARMER_NUM_WORK_DAYS = 3;
const int FARMER_NUM_REST_DAYS = 1;

// TODO: Start to implement your code.

Farmer::Farmer() : Employee(FARMER_COST, FARMER_SALARY, FARMER_NUM_WORK_DAYS, FARMER_NUM_REST_DAYS)
{
    
}

Farmer::~Farmer()
{
}
