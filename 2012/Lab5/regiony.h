#ifndef REGIONY_H
#define REGIONY_H

#include "organization.h"

class RegionY : public Organization
{
public:
	RegionY(const char* name, int num_patient, int num_death);
	~RegionY();
	void print_description() const override;
	bool report() const override;

private:
	int num_patient;
	int num_death;
};
#endif

