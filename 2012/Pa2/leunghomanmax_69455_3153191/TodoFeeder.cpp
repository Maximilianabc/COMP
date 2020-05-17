/*
 * File Created: Sun Mar 15 2020
 * Author:
 * SID:
*/

#include "TodoFeeder.h"

// Feeder cost 20 salary 5.
// Feeder 6 days work & 1 day rest.
const int FEEDER_COST = 20;
const int FEEDER_SALARY = 5;
const int FEEDER_NUM_WORK_DAYS = 6;
const int FEEDER_NUM_REST_DAYS = 1;
// TODO: Start to implement your code.

Feeder::Feeder() : Employee(FEEDER_COST, FEEDER_SALARY, FEEDER_NUM_WORK_DAYS, FEEDER_NUM_REST_DAYS)
{

}

Feeder::~Feeder()
{
}