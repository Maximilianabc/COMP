/*
 * COMP2011 (Fall 2019) Assignment 4: The Web System
 * File: pa4.cpp
 *
 * Student name: LEUNG Ho Man Max
 * Student ID: 20611398
 * Student email: hmmleung@connect.ust.hk
 *
 * Note: This is for your implementation.
 * Please first fill your information above.
 * Feel free to add your helper functions in pa4.cpp.
 * There is no need to change other files.
 */

#include "web.h"

WebNode::WebNode(const char* webAddress, const char* anchorText, int height)
{
    m_webAddress = strdup(webAddress);
    m_anchorText = strdup(anchorText);
    m_numOfHyperlinks = 0;
    m_hyperlink = nullptr;

    if (!isHTMLfile(webAddress)) return;
    if (height != 1)
    {      
        char** anchor_tag = nullptr;
        int num_tags = 0;
        extractAllAnchorTags(webAddress, anchor_tag, num_tags);

        m_hyperlink = new WebNode* [num_tags];
        m_numOfHyperlinks = num_tags;
        for (int i = 0; i < num_tags; i++)
        {
            const char* newaddress = getWebAddress(anchor_tag[i]);
            const char* newanchor = getAnchorText(anchor_tag[i]);
            m_hyperlink[i] = new WebNode(newaddress, newanchor, height - 1);
            delete newaddress;
            delete newanchor;
        }
        for (int i = 0; i < num_tags; i++)
        {
            delete anchor_tag[i];
        }
        delete[] anchor_tag;
    }
}

WebNode::~WebNode()
{
    delete m_webAddress;
    delete m_anchorText;
    for (int i = 0; i < m_numOfHyperlinks; i++)
    {
        delete m_hyperlink[i];
    }
    delete m_hyperlink;
}

void WebNode::printGraph(int depth) const
{
    cout << string(depth, '\t') << m_anchorText << " (url:" << m_webAddress << ")" << endl;
    if (m_numOfHyperlinks == 0) return;
    for (int i = 0; i < m_numOfHyperlinks; i++)
    {
        m_hyperlink[i]->printGraph(depth + 1);
    }
}

const WebNode* WebNode::findParent(const char* webAddress) const
{
    if (string(m_webAddress).compare(string(webAddress)) == 0)
    {
        if (!(string(m_anchorText).compare("Root") == 0))
        {
            return this;
        }
    }
    if (m_numOfHyperlinks == 0) return nullptr;
    for (int i = 0; i < m_numOfHyperlinks; i++)
    {
        const WebNode* node = m_hyperlink[i]->findParent(webAddress);
        if (node != nullptr)
        {
            if (!(string(node->m_webAddress).compare(string(webAddress)) == 0))
            {
                return node;
            }
            return this;
        }
    }
    return nullptr;
}

void WebNode::printPath(const char* address1, const char* address2) const
{
    const int CHAR_SIZE = 16;
    if (!(string(address1).compare(string(address2)) == 0))
    {
        bool ReachedBoundary = false;
        const char** paths = new const char* [CHAR_SIZE];
        paths[0] = address2;

        int k = 1;
        const WebNode* node = findParent(address2);
        while (node != nullptr && !ReachedBoundary)
        {
            paths[k] = node->m_webAddress;
            if (string(node->m_webAddress).compare(string(address1)) == 0)
            {
                ReachedBoundary = true;
            }
            else
            {
                node = findParent(node->m_webAddress);
                k++;
            }
        }
        if (!ReachedBoundary)
        {
            cout << "Path not found" << endl;
        }
        else
        {
            for (; k >= 0; k--)
            {
                cout << paths[k] << " ";
            }
            cout << endl;
        }
        for (int i = 0; i < CHAR_SIZE; i++)
        {
            delete[] paths[i];
        }  
        delete[] paths;
    }
    else
    {
        cout << address1 << endl;
    }
}