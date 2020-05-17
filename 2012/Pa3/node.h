//do NOT modify this file
//do NOT submit this file

#ifndef NODE_H
#define NODE_H

template <typename T>
class Tree;

//template of a tree Node that can store type T data
//you can assume the type T supports deep copy and assignment properly
//you can also assume type T has a default constructor
//for example, string (what we use in this assignment as T) supports all of above
//note that we implement the tree like the lecture note's BST implementation instead of the textbook/traditional one
//therefore, the children are Trees instead of Nodes
template <typename T>
class Node {
    friend class Tree<T>;
    friend class InfectionAnalyzer;
private:
    T data; //data
    int childCount = 0; //number of children
    Tree<T>* children = nullptr; //array of children, it must be just big enough to hold all the children

    Node() = default; 
    Node(const T& x) : data(x) { }; 
    Node(const T& x, int n) : data(x), childCount(n), children(new Tree<T> [n]) { };
	Node(const Node& a) : Node(a.data, a.childCount) 
    {
        for (int j = 0; j < childCount; ++j)
             children[j] = a.children[j]; //assume a proper assignment operator for Tree
    };
    ~Node() { delete [] children; }; //recursive delete - it relies on your Tree destructor
};

#endif