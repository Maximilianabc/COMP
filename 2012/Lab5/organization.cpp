#include <iostream>
#include <cstring>
#include <iomanip>

#include "organization.h"
#include "regionx.h"
#include "regiony.h"
#include "who.h"

using namespace std;

Organization::Organization(const char* name)
{
	this->name = new char[strlen(name) + 1];
	strcpy_s(this->name, strlen(name) + 1, name);
}

Organization::~Organization()
{
	cout << "Organization Dtor: "<< name << endl;
	delete[] name;
}

void Organization::print_description() const
{
	cout << "Organization: " << name;
}

bool Organization::report() const
{
	return false;
}            

RegionX::RegionX(const char* name, int num_patient, int num_death) : Organization::Organization(name)
{
	this->num_patient = num_patient;
	this->num_death = num_death;
}

RegionX::~RegionX()
{
	cout << "RegionX Dtor" << endl;
}

void RegionX::print_description() const
{
	Organization::print_description();
	cout << " Type: RegionX" << endl;
}

bool RegionX::report() const
{
	float deathrate = (float)num_death / (float)num_patient * 100;
	bool servere = deathrate > 3.0F;
	cout.precision(2);
	cout << "Death rate : death rate " << fixed << round(deathrate * 100) / 100 << (servere ? "% > 3%, " : "% <= 3%, not ") << "servere!" << endl;
	return servere;
}

RegionY::RegionY(const char* name, int num_patient, int num_death) : Organization::Organization(name)
{
	this->num_patient = num_patient;
	this->num_death = num_death;
}

RegionY::~RegionY()
{
	cout << "RegionY Dtor" << endl;
}

void RegionY::print_description() const
{
	Organization::print_description();
	cout << " Type: RegionY" << endl;
}

bool RegionY::report() const
{
	float deathrate = (float)num_death / (float)num_patient * 100;
	bool servere = deathrate > 4.0F;
	cout.precision(2);
	cout << "Death rate : death rate " << fixed << round(deathrate * 100) / 100 << (servere ? "% > 4%, " : "% <= 4%, not ") << "servere!" << endl;
	return servere;
}

WHO::WHO(const char* name) : Organization::Organization(name)
{
	num_watchlist = 0;
	for (int i = 0; i < MAX_NUM_WATCHLIST; i++)
	{
		this->watchlist[i] = nullptr;
	}
}

WHO::~WHO()
{
	for (int i = 0; i < num_watchlist; i++)
	{
		delete watchlist[i];
	}
	cout << "WHO Dtor" << endl;
}

void WHO::print_description() const
{
	Organization::print_description();
	cout << " Type: WHO" << endl;
}

void WHO::watch(Organization* new_organization)
{
	watchlist[num_watchlist] = new_organization;
	num_watchlist++;
}

bool WHO::report() const
{
	int num_servere = 0;
	for (int i = 0; i < num_watchlist; i++)
	{
		watchlist[i]->print_description();
		if (watchlist[i]->report())  num_servere++;
	}

	bool servere = (float)num_servere / (float)num_watchlist > 0.5F;
	cout << "Severe regions: " << num_servere << "/" << num_watchlist << ", " << (servere ? "" : "not ") << "servere!" << endl;
	return servere;
}