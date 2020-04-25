#ifndef REGIONX_H
#define REGIONX_H

#include "organization.h"

class RegionX : public Organization
{
public:
	RegionX(const char* name, int num_patient, int num_death);
	~RegionX();
	void print_description() const override;
	bool report() const override;

private:
	int num_patient;
	int num_death;
};
#endif
