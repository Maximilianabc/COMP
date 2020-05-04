#include <iostream>
#include <string>

using namespace std;

ostream& operator<<(ostream& os, pair<string, string> value) {
	os << "\n(" << value.first << " " << value.second << ")";
	return os;
}

#include "set.h"

int main() {

	cout << "==========================================\n"
			<< "||             Phone books              ||\n"
			<< "==========================================\n" << endl;
	int emergencyNumberUSA[3] = { 911, 311, 112 };
	int emergencyNumberHK1[3] = { 112, 999, 27353355 };
	int emergencyNumberHK2[3] = { 999, 112, 27353355 };
	set<int> A(emergencyNumberHK1, 3);
	set<int> B(emergencyNumberUSA, 3);
	set<int> D(emergencyNumberHK2, 3);

	cout << "1. Alice, Bob, and Dan are sharing their phone books." << endl;

	cout << "Alice's phone book (A) : ";
	cout << A << endl;
	cout << "|A| = " << A.cardinality() << endl << endl;

	cout << "Bob's phone book (B) : ";
	cout << B << endl;
	cout << "|B| = " << B.cardinality() << endl << endl;

	cout << "Dan's phone book (D) : ";
	cout << D << endl;
	cout << "|D| = " << D.cardinality() << endl << endl;

	set<int> C = A + B;
	cout
			<< "2. Carol collects the emergency contacts from Alice and Bob. (A union B)"
			<< endl;
	cout << "Carol's phone book (C) : ";
	cout << C << endl;
	cout << "|C| = " << C.cardinality() << endl << endl;

	set<int> E = A * B;
	cout
			<< "3. Eve wants to find the number(s) that would work for both Alice and Bob. (A intersect B)"
			<< endl;
	cout << "Eve's phone book (E) : ";
	cout << E << endl;
	cout << "|E| = " << E.cardinality() << endl << endl;

	cout
			<< "4. Alice, Bob, and Dan want to find out if their phone books share the same numbers."
			<< endl;
	cout << "A = ";
	cout << A << endl;
	cout << "B = ";
	cout << B << endl;
	cout << "D = ";
	cout << D << endl;
	cout << "A == B? " << ((A == B) ? "true" : "false") << endl;
	cout << "A == D? " << ((A == D) ? "true" : "false") << endl;

	cout << endl;
	cout << "==========================================\n"
			<< "||           Useful  Websites           ||\n"
			<< "==========================================\n" << endl;
	set<pair<string, string>> directory;

	pair<string, string> ust { "HKUST news", "https://www.ust.hk/news" };
	pair<string, string> chp { "Centre for Health Protection (HK)",
			"https://www.chp.gov.hk/" };
	pair<string, string> ust_portal { "HKUST official figures",
			"https://hseo.ust.hk/ncov" };
	pair<string, string> ust_portal_mispelled { "HKUST official figures",
			"https://hseo.ust.hk/SARS-Cov-2" };

	cout << "5. The initial state of directory : " << endl;
	cout << directory << endl << endl;
	cout << "6. Adding some useful websites : " << endl;
	directory.addElement(ust);
	directory.addElement(chp);

	cout << directory << endl << endl;

	cout
			<< "7. Several students have suggested the portal to the HKUST official figures : "
			<< endl;
	directory.addElement(ust_portal);
	directory.addElement(ust_portal);
	directory.addElement(ust_portal);
	directory.addElement(ust_portal_mispelled);
	cout << directory << endl << endl;

	cout << "8. Oops, a student misspelled the website, let's remove that : "
			<< endl;
	directory.removeElement(ust_portal_mispelled);
	cout << directory << endl << endl;

	return 0;
}
