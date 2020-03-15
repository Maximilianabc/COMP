/*
 * COMP2011 (Fall 2019) Assignment 4: The Web System
 * File: web.cpp
 * Note: The implementation of functions for reading HTML files.
 * There is no need to change this file.
 */
#include <cstdlib>
#include "web.h"

 /*
  * web.cpp contains helper functions for the Web System,
  * while you can use the following functions for implementing the required tasks,
  * Do NOT modify it in any way.
  */

  /*
   * Checking if a starting anchor tag exists in the text content.
   * Paramters:
   *    const char* content: the text content
   * Return true if there exists an starting anchor tag in the content character string;
   * otherwise, return false
   */
bool hasNextStartAnchorTag(const char* content)
{
    const char* startCharPtr = strstr(content, "<a"); // the open anchor tag
    return (startCharPtr != nullptr);
}

/*
 * Checking if an closing anchor tag exists in the text content.
 * Paramters:
 *    const char* content: the text content
 * Return true if there exists an closing anchor tag in the content character string;
 * otherwise, return false
 */
bool hasNextEndAnchorTag(const char* content)
{
    const char* endCharPtr = strstr(content, "</a>"); // the close anchor tag
    return (endCharPtr != nullptr);
}

const char* endAnchorTagPosition(const char* content)
{
    const char* endCharPtr = strstr(content, "</a>"); // the close anchor tag
    return (endCharPtr + strlen("</a>"));
}


/*
 * Find the anchor tag in the text content and allocate the c string to store
 * the anchor tag,
 *   i.e. <a href="..."> ... </a>
 * in the character pointer, anchorTag
 * Parameters:
 *   const char* content: the text content
 *   char* &anchorTag: for return the anchor tag extracted
 * Return Value: the ending position of the tag
 */
void extractNextAnchorTag(const char* content, char*& anchorTag)
{
    const char* startCharPtr = strstr(content, "<a");
    const char* endCharPtr = strstr(content, "</a>");
    int tagLength = endCharPtr - startCharPtr + strlen("</a>");

    anchorTag = new char[tagLength + 1];
    strncpy(anchorTag, startCharPtr, tagLength);
    anchorTag[tagLength] = '\0';
}


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
void extractAllAnchorTags(const char* webAddress, char**& anchorTags, int& numOfAnchorTags)
{
    ifstream myfile;
    char line[256];
    char content[2048] = "";

    numOfAnchorTags = 0;
    myfile.open(webAddress);

    if (myfile.is_open())
    {
        myfile >> line;

        while (!myfile.eof())
        {
            // check if the line has the starting anchor tag
            if (hasNextStartAnchorTag(line))
            {
                strcpy(content, "");
                // continue to read the next line until a closing anchor tag is found
                while (!myfile.eof() && !hasNextEndAnchorTag(line))
                {
                    if ((strlen(content) + strlen(line) + 1) >= 2048)
                    {
                        cerr << "Error: The content line is too long." << endl;
                        break;
                    }
                    strcat(content, line);
                    strcat(content, " ");
                    myfile >> line;
                }

                // allocate one more character string entry to the anchorTags array
                strcat(content, line);
                char** temp = anchorTags;
                anchorTags = new char* [numOfAnchorTags + 1];

                for (int j = 0; j < numOfAnchorTags; j++)
                {
                    anchorTags[j] = new char[strlen(temp[j]) + 1];
                    strcpy(anchorTags[j], temp[j]);
                }

                for (int j = 0; j < numOfAnchorTags; j++)
                {
                    if (temp != nullptr) delete[] temp[j];
                    temp[j] = nullptr;
                }

                if (temp != nullptr) delete[] temp;
                temp = nullptr;

                // extract the whole anchor tag element and add it to the anchorTags array 
                extractNextAnchorTag(content, anchorTags[numOfAnchorTags]);
                strcpy(line, endAnchorTagPosition(content));
                numOfAnchorTags++;
            }
            else
                myfile >> line;
        }

        myfile.close();
    }
    else
    {
        cerr << "Error: Can't open \"" << webAddress << "\"" << endl;
        exit(1);
    }
}


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
bool isHTMLfile(const char* webAddress)
{
    const char* temp = strstr(webAddress, ".html");

    if ((temp != nullptr) && (strlen(temp) == 5))
        return true;
    else
    {
        temp = strstr(webAddress, ".htm");
        if ((temp != nullptr) && (strlen(temp) == 4))
            return true;
    }
    return false;
}


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
const char* getAnchorText(const char* anchorTag)
{
    char* text = nullptr;
    const char* startCharPtr = strstr(anchorTag, "href=\"");

    if (startCharPtr != nullptr)
    {
        startCharPtr += strlen("href=\"");
        startCharPtr = strstr(startCharPtr, ">");

        if (startCharPtr != nullptr)
            startCharPtr += strlen(">");

        // removing leading spaces
        while (*startCharPtr == ' ')
            startCharPtr++;

        const char* endCharPtr = strstr(startCharPtr, "</a>");
        int anchorTagLength = endCharPtr - startCharPtr;
        text = new char[anchorTagLength + 1];
        strncpy(text, startCharPtr, anchorTagLength);
        text[anchorTagLength] = '\0';

        // removing trailing spaces
        while ((anchorTagLength > 1) && (text[anchorTagLength - 1] == ' '))
        {
            anchorTagLength--;
            text[anchorTagLength] = '\0';
        }
    }

    return (text);
}


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
const char* getWebAddress(const char* anchorTag)
{
    char* text = nullptr;
    const char* startCharPtr = strstr(anchorTag, "href=\"");

    if (startCharPtr != nullptr)
    {
        startCharPtr += strlen("href=\"");

        // removing leading spaces
        while (*startCharPtr == ' ')
            startCharPtr++;

        const char* endCharPtr = strstr(startCharPtr, "\"");
        int anchorTagLength = endCharPtr - startCharPtr;
        text = new char[anchorTagLength + 1];
        strncpy(text, startCharPtr, anchorTagLength);
        text[anchorTagLength] = '\0';

        // removing trailing spaces
        while ((anchorTagLength > 1) && (text[anchorTagLength - 1] == ' '))
        {
            anchorTagLength--;
            text[anchorTagLength] = '\0';
        }
    }

    return (text);
}