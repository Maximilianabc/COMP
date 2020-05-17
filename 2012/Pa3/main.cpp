/*
 * Do NOT submit this file
 * 
 * while you can and you should create your own test cases to test your own
 * program, please make sure your uploaded source code can be compiled with
 * the original version of main.cpp and the header files on our Windows Eclipse.
 */ 

#include <iostream>
#include <string>
#include "tree.h"
#include "infectionAnalyzer.h"

using namespace std;

template <typename T>
ostream& operator<<(ostream& os, const Tree<T>& t)
{
    t.print(os);
	return os;
}

int main()
{
    cout << "The Infection Analyzer" << endl << endl;
    cout << boolalpha;

    Tree<string> tree;
    cout << "===================================================" << endl;
    cout << "Testing tree.addRoot..." << endl;
    cout << "tree.addRoot(\"A\") returns " << tree.addRoot("A") << endl;
    cout << "===================================================" << endl;
    cout << "Testing tree.addChild..." << endl;
    cout << "tree.addChild(\"A\",\"B\") returns " << tree.addChild("A","B") << endl;
    cout << "tree.addChild(\"A\",\"C\") returns " << tree.addChild("A","C") << endl;
    cout << "tree.addChild(\"A\",\"D\") returns " << tree.addChild("A","D") << endl;
    cout << "tree.addChild(\"C\",\"C1\") returns " << tree.addChild("C","C1") << endl;
    cout << "tree.addChild(\"C\",\"C2\") returns " << tree.addChild("C","C2") << endl;
    cout << "tree.addChild(\"K\",\"KK\") returns " << tree.addChild("K","KK") << endl;
    cout << endl << tree << endl;

    cout << "===================================================" << endl;
    cout << "Testing tree.getDepth..." << endl;
    cout << "tree.getDepth(\"A\") returns " << tree.getDepth("A") << endl;
    cout << "tree.getDepth(\"B\") returns " << tree.getDepth("B") << endl;
    cout << "tree.getDepth(\"C\") returns " << tree.getDepth("C") << endl;
    cout << "tree.getDepth(\"C1\") returns " << tree.getDepth("C1") << endl;
    cout << "tree.getDepth(\"C2\") returns " << tree.getDepth("C2") << endl;
    cout << "tree.getDepth(\"D\") returns " << tree.getDepth("D") << endl;
    cout << "tree.getDepth(\"Z\") returns " << tree.getDepth("Z") << endl;

    cout << "===================================================" << endl;
    cout << "Testing tree.getDescendantCount..." << endl;
    cout << "tree.getDescendantCount(\"A\") returns " << tree.getDescendantCount("A") << endl;
    cout << "tree.getDescendantCount(\"B\") returns " << tree.getDescendantCount("B") << endl;
    cout << "tree.getDescendantCount(\"C\") returns " << tree.getDescendantCount("C") << endl;
    cout << "tree.getDescendantCount(\"C1\") returns " << tree.getDescendantCount("C1") << endl;
    cout << "tree.getDescendantCount(\"C2\") returns " << tree.getDescendantCount("C2") << endl;
    cout << "tree.getDescendantCount(\"D\") returns " << tree.getDescendantCount("D") << endl;
    cout << "tree.getDescendantCount(\"Z\") returns " << tree.getDescendantCount("Z") << endl;

    cout << "===================================================" << endl;
    cout << "Testing Tree's deep copy constructor" << endl;
    Tree<string> tree2(tree);
    tree.addChild("C","C3");
    cout << endl << tree << endl;
    cout << endl << tree2 << endl;

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer1.loadInfectionFile..." << endl;
    InfectionAnalyzer infectionAnalyzer1;
    bool result = infectionAnalyzer1.loadInfectionFile("infections1.txt");
    if (!result)
    {
        cout << "For Windows Eclipse 4.6.2, put the txt files under the same folder as your source files." << endl;
        cout << "For other versions of Eclipse, you may try to put the files under the project / base directory." << endl;
        cout << "If you need to use other IDEs/OSes, please look online and figure out where the input file should be put." << endl;
        return 0;
    }
    cout << "Loaded." << endl;
    infectionAnalyzer1.printInfectionTrees(cout);

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer2a.loadInfectionFile with tricky input..." << endl;
    InfectionAnalyzer infectionAnalyzer2a;
    cout << "Loaded? " << infectionAnalyzer2a.loadInfectionFile("infections2.txt") << endl;
    infectionAnalyzer2a.printInfectionTrees(cout);

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer2b.loadInfectionFile with tricky input..." << endl;
    InfectionAnalyzer infectionAnalyzer2b;
    cout << "Loaded? " << infectionAnalyzer2b.loadInfectionFile("infections4.txt") << endl;
    infectionAnalyzer2b.printInfectionTrees(cout);

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer1.isInfected..." << endl;
    infectionAnalyzer1.printInfectionTrees(cout);
    cout << "infectionAnalyzer1.isInfected(\"A\") returns \"" << infectionAnalyzer1.isInfected("A") << "\"" << endl;
    cout << "infectionAnalyzer1.isInfected(\"B\") returns \"" << infectionAnalyzer1.isInfected("B") << "\"" << endl;
    cout << "infectionAnalyzer1.isInfected(\"X\") returns \"" << infectionAnalyzer1.isInfected("X") << "\"" << endl;

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer1.getInfectionSource..." << endl;
    infectionAnalyzer1.printInfectionTrees(cout);
    cout << "infectionAnalyzer1.getInfectionSource(\"A\") returns \"" << infectionAnalyzer1.getInfectionSource("A") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"B\") returns \"" << infectionAnalyzer1.getInfectionSource("B") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"B1\") returns \"" << infectionAnalyzer1.getInfectionSource("B1") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"B2\") returns \"" << infectionAnalyzer1.getInfectionSource("B2") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"B3\") returns \"" << infectionAnalyzer1.getInfectionSource("B3") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"B4\") returns \"" << infectionAnalyzer1.getInfectionSource("B4") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"C\") returns \"" << infectionAnalyzer1.getInfectionSource("C") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"C1\") returns \"" << infectionAnalyzer1.getInfectionSource("C1") << "\"" << endl;
    cout << "infectionAnalyzer1.getInfectionSource(\"C2\") returns \"" << infectionAnalyzer1.getInfectionSource("C2") << "\"" << endl;

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer3.getInfectionSource..." << endl;
    InfectionAnalyzer infectionAnalyzer3;
    cout << "Loaded? " << infectionAnalyzer3.loadInfectionFile("infections3.txt") << endl;
    infectionAnalyzer3.printInfectionTrees(cout);
    cout << "infectionAnalyzer3.getInfectionSource(\"A\") returns \"" << infectionAnalyzer3.getInfectionSource("A") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"B\") returns \"" << infectionAnalyzer3.getInfectionSource("B") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"C\") returns \"" << infectionAnalyzer3.getInfectionSource("C") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"F\") returns \"" << infectionAnalyzer3.getInfectionSource("F") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"G\") returns \"" << infectionAnalyzer3.getInfectionSource("G") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"I\") returns \"" << infectionAnalyzer3.getInfectionSource("I") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionSource(\"J\") returns \"" << infectionAnalyzer3.getInfectionSource("J") << "\"" << endl;

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer3.getInfectionGeneration..." << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"A\") returns \"" << infectionAnalyzer3.getInfectionGeneration("A") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"B\") returns \"" << infectionAnalyzer3.getInfectionGeneration("B") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"C\") returns \"" << infectionAnalyzer3.getInfectionGeneration("C") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"F\") returns \"" << infectionAnalyzer3.getInfectionGeneration("F") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"G\") returns \"" << infectionAnalyzer3.getInfectionGeneration("G") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"I\") returns \"" << infectionAnalyzer3.getInfectionGeneration("I") << "\"" << endl;
    cout << "infectionAnalyzer3.getInfectionGeneration(\"J\") returns \"" << infectionAnalyzer3.getInfectionGeneration("J") << "\"" << endl;

    cout << "===================================================" << endl;
    cout << "Testing infectionAnalyzer3.getInfectionPowerVector..." << endl;
    const vector<pair<string,int>>& v = infectionAnalyzer3.getInfectionPowerVector();
    cout << endl << "Infection power chart:" << endl;
    for (pair<string,int> p: v)
        cout << "(" << p.first << "," << p.second << ")" << endl;

    cout << "===================================================" << endl;
    cout << "Special case analysis: hotpot family" << endl;
    InfectionAnalyzer hotpotInfectionAnalyzer;
    hotpotInfectionAnalyzer.loadInfectionFile("hotpot.txt");
    hotpotInfectionAnalyzer.printInfectionTrees(cout);
    const vector<pair<string,int>>& v2 = hotpotInfectionAnalyzer.getInfectionPowerVector();
    cout << endl << "Infection power chart:" << endl;
    for (pair<string,int> p: v2)
        cout << "(" << p.first << "," << p.second << ")" << endl;
    
    cout << "===================================================" << endl;
    cout << "Done! Please take care, and stay healthy! :)" << endl;
    return 0;
}