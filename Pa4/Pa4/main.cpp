/*
 * COMP2011 (Fall 2019) Assignment 4: The Web System
 * File: main.cpp
 * Note: The main function for PA4.
 * It is no need to change this file.
 */
#include "web.h"

int main()
{
    const int MAX_LEN = 100;
    char filename[MAX_LEN + 1] = "";
    char anchorText[MAX_LEN + 1] = "Root";
    int max_height = 0;
    WebNode* rootPage = nullptr; // Empty WebNode

    // Print the program title
    cout << "Assignment 4: The Web System" << endl;
    cout << endl;

    // Get the homepage (rootpage) 
    cout << "Input the homepage address: ";
    cin >> filename;
    ifstream ifs(filename);
    if (!ifs)
    {
        cerr << "Error: Can't open \"" << filename << "\"" << endl;
        return -1;
    }

    // Get the maximum height of the Web graph
    cout << endl << "Input the maximum height: ";
    cin >> max_height;
    if (max_height <= 0)
    {
        cerr << "Error: The input height must greater than 0" << endl;
        return -1;
    }

    // Create the Web
    rootPage = new WebNode(filename, anchorText, max_height);
    cout << endl;

    // Print the Web
    cout << "Print the Web: " << endl;
    rootPage->printGraph(0);
    cout << endl;

    cout << "To find the parent of the webpage, input the address: ";
    cin >> filename;
    const WebNode* node2 = rootPage->findParent(filename);
    if (node2 != nullptr)
        cout << "The parent file is " << node2->webAddress() << endl;
    else
        cout << "Parent not found" << endl;
    cout << endl;

    char filename1[MAX_LEN + 1], filename2[MAX_LEN + 1];
    cout << "To print the path between two pages," << endl;
    cout << "input the 1st address: ";
    cin >> filename1;
    cout << "input the 2nd address: ";
    cin >> filename2;
    rootPage->printPath(filename1, filename2);
    cout << endl;

    // Exit the program
    delete rootPage;
    return 0;
}