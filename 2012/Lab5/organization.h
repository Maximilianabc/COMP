#ifndef ORGANIZATION_H
#define ORGANIZATION_H

class Organization {
public:
	Organization(const char* name);
	virtual ~Organization();
	virtual void print_description() const;
	virtual bool report() const;

private:
	char* name;
};
#endif
