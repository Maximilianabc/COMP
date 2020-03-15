/*
 * COMP2011 (Fall 2019) Assignment 4: The Web System
 * File: web.h
 * Note: This is the header file for your PA4.
 * You are NOT allowed to change this file.
 */
#ifndef WEB_H
#define WEB_H

#include <iostream>
#include <fstream>
#include <cstring>

using namespace std;

class WebNode
{
private:
    char* m_webAddress;
    char* m_anchorText;
    WebNode** m_hyperlink;
    int m_numOfHyperlinks;

public:
    // CONSTRUCTOR member function
    WebNode(const char* webAddress, const char* anchorText, int height);
    // DESTRUCTOR member function
    ~WebNode();
    // ACCESSOR member functions
    const char* webAddress() const { return m_webAddress; }
    const char* anchorText() const { return m_anchorText; }
    const WebNode* const* hyperlink() const { return m_hyperlink; }
    int numOfHyperlinks() const { return m_numOfHyperlinks; }

    void printGraph(int depth = 0) const;
    const WebNode* findParent(const char* webAddress) const;
    void printPath(const char* address1, const char* address2) const;
};

/* Functions provided */

/*
 * Given a webAddress, read the text from the file and extract all the anchor tags.
 * Parameters:
 *   const char* webAddress: the name of page file to be processed
 *   char**& anchorTags: a dynamically created array of c strings for storing and returning the anchor tags extracted
 *   int& numOfAnchorTags: for returning the number of links extracted
 * Usage example:
 *   char** anchor_tag = nullptr;
 *   int num_tags = 0;
 *   extractAllAnchorTags("input.html", anchor_tag, num_tags);
 *   cout << "anchor_tags are "<<endl;
 *   for (int i=0; i<num_tags; i++)
 *        cout<<anchor_tag[i]<<endl;
 * Example output:
 *   anchor_tags are
 *   <a href="input1.html">first link</a>
 *   <a href="input2.html">second link</a>
 *   <a href="input3.htm">third link</a>
 */
void extractAllAnchorTags(const char* webAddress, char**& anchorTags, int& numOfAnchorTags);

/*
 * Check if the webAddress has the extension .htm or .html
 * Parameters:
 *   const char* webAddress: the name of page file to be processed
 * Returns true if it is an HTML file; otherwise, return false.
 * Usage Example:
 *   cout << (isHTMLfile("input.html")? "True": "False") << endl;
 *   cout << (isHTMLfile("input3.htm")? "True": "False") << endl;
 *   cout << (isHTMLfile("main.cpp")? "True": "False") << endl;
 *   cout << (isHTMLfile("http://google.com")? "True": "False") << endl;
 * Example Output:
 *   True
 *   True
 *   False
 *   False
 */
bool isHTMLfile(const char* webAddress);

/*
 * Extract the anchor text from an achor tag element
 * Parameters:
 *   const char* anchorTag: the whole tag element
 * Return the anchor text (dynamic memory) in the anchor anchorTag
 * Usage Example:
 *   const char* anchorTag = "<a href=\"input1.html\">first link</a>";
 *   cout << getAnchorText(anchorTag) << endl;
 * Example Output:
 *   first link
 */
const char* getAnchorText(const char* anchorTag);

/*
 * Extract the web address(URL) from an achor tag element
 * Parameters:
 *   const char* anchorTag: the whole tag element
 * Return the web address (URL) (dynamic memory) in the anchor anchorTag
 * Usage Example:
 *   const char* anchorTag = "<a href=\"input1.html\">first link</a>";
 *   cout << getWebAddress(anchorTag) << endl;
 * Example Output:
 *   input1.html
 */
const char* getWebAddress(const char* anchorTag);
#endif
