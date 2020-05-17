#include "tree.h"
//this is one of the two files that you need to submit

using namespace std;
//you are NOT allowed to include additional libraries yourself

//you do NOT need to include "tree.h" in this file
//this is NOT a cpp file, but simply a part of "tree.h"
//see the bottom of tree.h for explanation
//just write your tree implementation here right away

template<typename T>
inline Tree<T>::Tree(const Tree& another)
{
}

template<typename T>
Tree<T>::~Tree()
{
}

template<typename T>
inline const Tree<T>& Tree<T>::operator=(const Tree& another)
{
    // TODO: insert return statement here
}

template<typename T>
inline Tree<T>* Tree<T>::find(const T& data)
{
    return NULL;
}

template<typename T>
inline const Tree<T>* Tree<T>::find(const T& data) const
{
    return NULL;
}

template<typename T>
inline int Tree<T>::getDepth(const T& data) const
{
    return 0;
}

template<typename T>
inline int Tree<T>::getDescendantCount(const T& data) const
{
    return 0;
}

template<typename T>
inline int Tree<T>::getDescendantCount() const
{
    return 0;
}

template<typename T>
inline bool Tree<T>::addRoot(const T& data)
{
    return false;
}

template<typename T>
inline bool Tree<T>::addChild(const T& parentData, const T& childData)
{
    return false;
}


