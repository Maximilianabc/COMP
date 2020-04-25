#ifndef WHO_H
#define WHO_H

#include "organization.h"

const int MAX_NUM_WATCHLIST = 5;
class WHO : public Organization
{
public:
	WHO(const char* name);
	~WHO();
	void print_description() const override;
	void watch(Organization* new_organization);
	bool report() const override;

private:
	Organization* watchlist[MAX_NUM_WATCHLIST];
	int num_watchlist = 0;
};
#endif

