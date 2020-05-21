//this is one of the two files that you need to submit

#include "infectionAnalyzer.h"
#include <fstream> //for reading file
#include <queue> //you may use STL queue
#include <algorithm> //you may use STL algorithm
using namespace std;
//you are NOT allowed to include additional libraries yourself

InfectionAnalyzer::~InfectionAnalyzer()
{
    trees.clear();
    infectionPowerVector.clear();
}

bool InfectionAnalyzer::loadInfectionFile(const string filename)
{
    string parent;
    string child;
    ifstream fin;

    fin.open(filename);
    if (fin.fail()) return false;

    while (fin >> parent >> child)
    {
        if (trees.empty())
        {
            Tree<string>* tree = new Tree<string>();
            tree->addRoot(parent);
            tree->addChild(parent, child);
            trees.push_back(tree);
        }
        else
        {
            Tree<string>* p = nullptr;
            Tree<string>* c = nullptr;
            int pindex = -1;
            int cindex = -1;
            for (size_t i = 0; i < trees.size(); i++)
            {
                if (p == nullptr) 
                    p = trees[i]->find(parent);
                if (p != nullptr && pindex == -1) 
                    pindex = i;
                if (c == nullptr) 
                    c = trees[i]->find(child);
                if (c != nullptr && cindex == -1) 
                    cindex = i;               
            }
            if (p == nullptr && c == nullptr)
            {
                Tree<string>* tree = new Tree<string>();
                tree->addRoot(parent);
                tree->addChild(parent, child);
                trees.push_back(tree);
            }
            else if (p != nullptr && c != nullptr)
            {
                if (pindex == cindex) return false;
                else
                {
                    Tree<string> copy(*trees[cindex]);
                    Tree<string>* n = new Tree<string>[p->root->childCount + 1];
                    n->root = new Node<string>(*p->root);

                    for (int i = 0; i < n->root->childCount; i++)
                    {
                        n->root->children[i] = p->root->children[i];
                    }
                    n->root->children[n->root->childCount] = copy;
                    n->root->childCount++;
                    *p = *n;
                    trees.erase(trees.begin() + cindex);
                    trees.shrink_to_fit();
                }
            }
            else if (p != nullptr)
            {
                p->addChild(parent, child);
            }
            else if (c != nullptr)
            {
                c->addRoot(parent);
            }
        }
    }
    return true;
}

bool InfectionAnalyzer::isInfected(const string name) const
{
    for (size_t i = 0; i < trees.size(); i++)
    {
        if (trees[i]->find(name) != nullptr) return true;
    }
    return false;
}

string InfectionAnalyzer::getInfectionSource(const string name) const
{
    Tree<string>* t = nullptr;
    for (size_t i = 0; i < trees.size(); i++)
    {
        if (trees[i]->find(name) != nullptr)
        {
            t = trees[i];
            break;
        }
    }
    if (t == nullptr) return "NA";
    if (t->root->childCount == 0) return t->root->data;

    string parent;
    vector<Tree<string>*> v;
    for (int i = 0; i < t->root->childCount; i++)
    {
        v.push_back(&t->root->children[i]);
    }
    for (size_t i = 0; i < v.size(); i++)
    {
        if (v[i]->find(name) != nullptr)
        {
            if (v[i]->root->data == name) return t->root->data;
            else
            {
                t = v[i];
                v.push_back(t);
                for (int k = 0; k < t->root->childCount; k++)
                {               
                    v.push_back(&t->root->children[k]);
                }     
                i = 0;
                v.erase(v.begin(), v.begin() + v.size() - t->root->childCount - 1);
                v.shrink_to_fit();
                t = v[0];
            }
        }
    }
    return "ZERO";
}

int InfectionAnalyzer::getInfectionGeneration(const string name) const
{
    Tree<string>* t = nullptr;
    for (size_t i = 0; i < trees.size(); i++)
    {
        int depth = trees[i]->getDepth(name);
        if (depth != -1) return depth;
    }
    return -1;
}

bool sortByPower(const pair<string, int>& a, const pair<string, int>& b)
{
    return a.second > b.second;
}

const vector<pair<string, int>>& InfectionAnalyzer::getInfectionPowerVector()
{
    int k = 0; 
    Tree<string>* t = nullptr;
    vector<Tree<string>*> v;
    for (int i = 0; i < trees.size(); i++)
    {      
        v.insert(v.begin(), 1, trees[i]);
        t = v[0];
        Tree<string>* tt = nullptr;
        Tree<string>* p = v[0];

        int index = 0;
        int j = 0;
        while(j < t->root->childCount)
        {
            string parent = getInfectionSource(t->root->data);
            p = parent == "ZERO" ? trees[i] : trees[i]->find(parent);
            tt = &t->root->children[j];
            v.insert(v.begin(), 1, tt);
            if (tt->root->childCount != 0)
            {
                index = j + 1;
                j = 0;
                t = v[0];
                continue;
            }
            if (j == t->root->childCount - 1)
            {
                if (t == p) break;
                t = p;
                j = index;
                continue;
            }
            j++;
        }
    }
    for (size_t k = 0; k < v.size(); k++)
    {
        infectionPowerVector.insert(infectionPowerVector.begin(), 1, make_pair(v[k]->root->data, v[k]->getDescendantCount()));
    }
    sort(infectionPowerVector.begin(), infectionPowerVector.end(), sortByPower);
    return infectionPowerVector;
}
