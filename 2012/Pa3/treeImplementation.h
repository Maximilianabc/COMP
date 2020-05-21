#include "tree.h"
//this is one of the two files that you need to submit

using namespace std;
//you are NOT allowed to include additional libraries yourself

//you do NOT need to include "tree.h" in this file
//this is NOT a cpp file, but simply a part of "tree.h"
//see the bottom of tree.h for explanation
//just write your tree implementation here right away

template<typename T>
Tree<T>::Tree(const Tree& another)
{
    if (another.isEmpty()) return;
    root = new Node<T>(*(another.root));
    if (root->childCount == 0) root->children = nullptr;
}

template<typename T>
Tree<T>::~Tree()
{
    if (root == nullptr) return;
    delete root;
}

template<typename T>
const Tree<T>& Tree<T>::operator=(const Tree& another)
{
    if (root != nullptr && root == another.root) return another;
    root = new Node<T>(*(another.root));
    if (root->childCount == 0) root->children = nullptr;
    return *this;
}

template<typename T>
Tree<T>* Tree<T>::find(const T& data)
{
    if (root == nullptr) return nullptr;
    if (root->data == data) return this;

    for (int i = 0; i < root->childCount; i++)
    {
        Tree<T>* found = root->children[i].find(data);
        if (found != nullptr) return found;
    }
    return nullptr;
}

template<typename T>
const Tree<T>* Tree<T>::find(const T& data) const
{
    if (root == nullptr) return nullptr;
    if (root->data == data) return this;

    for (int i = 0; i < root->childCount; i++)
    {
        const Tree<T>* found = root->children[i].find(data);
        if (found != nullptr) return found;
    }
    return nullptr;
}

template<typename T>
int Tree<T>::getDepth(const T& data) const
{
    int depth = 0;
    if (isEmpty()) return -1;
    if (root->data == data) return 0;
    else if (root->childCount == 0) return -1;
    else
    {
        for (int i = 0; i < root->childCount; i++)
        {
            depth++;
            int d = root->children[i].getDepth(data);
            if (d == 0) return depth;
            depth += d;          
        }
    }
    return depth > 0 ? depth : -1;
}

template<typename T>
int Tree<T>::getDescendantCount(const T& data) const
{
    const Tree<T>* child = find(data);
    if (child == nullptr) return -1;
    return child->getDescendantCount();
}

template<typename T>
int Tree<T>::getDescendantCount() const
{
    int descend = 0;
    for (int i = 0; i < root->childCount; i++)
    {
        descend++;
        descend += root->children[i].getDescendantCount();
    }
    return descend;
}

template<typename T>
bool Tree<T>::addRoot(const T& data)
{
    if (find(data)) return false;
    if (root == nullptr)
    {
        root = new Node<T>(data);
    }
    else
    {
        if (root->data == data) return false;
        Tree<T> newchild(*this);
        root = new Node<T>(data);
        root->children = new Tree<T>[++root->childCount];
        root->children[0] = newchild;
    }
    return true;
}

template<typename T>
bool Tree<T>::addChild(const T& parentData, const T& childData)
{
    Tree<T>* child = find(childData);
    Tree<T>* parent = find(parentData);
    if (child || !parent) return false;

    if (parent->root->children == nullptr)
    {
        parent->root->children = new Tree<T>[++parent->root->childCount];
        parent->root->children->root = new Node<T>(childData);
    }
    else
    {
        Tree<T>* copy = new Tree<T>[parent->root->childCount + 1];
        copy->root = new Node<T>(*parent->root);
        copy->root->children[parent->root->childCount].root = new Node<T>(childData);
        copy->root->childCount = parent->root->childCount + 1;
        *parent = *copy;
    }
    return true;
}


