//do NOT modify this file
//do NOT submit this file

#ifndef TREE_H
#define TREE_H

#include "node.h"
#include <iostream>
#include <string>

//template of a Tree that can store type T data
//you can assume the type T supports deep copy and assignment properly
//for example, string (what we use in this assignment as T) supports both
//you can also assume data are distinct in the tree, i.e., no two data in different nodes are the same
template <typename T>
class Tree {
friend class InfectionAnalyzer;

public:
    //let's keep the default constructor
    Tree() = default;

    //deep copy constructor
    //hint: as our general Tree implementation is inspired by the lecture notes's BST's, check it out for ideas
    Tree(const Tree& another);

    //destructor 
    //hint: as our general Tree implementation is inspired by the lecture notes's BST's, check it out for ideas
    ~Tree();

    //deep assignment
    //remember to check for self-assignment: if it is a self-assignmnt, do NOT copy
    const Tree& operator=(const Tree& another);

    //return true if the tree is empty, false otherwise
    bool isEmpty() const { return root==nullptr; };

    //search in the whole tree
    //and return the tree pointer to the tree with the root node that contains the specified data 
    //return nullptr if the specified data is not found
    Tree* find(const T& data);

    //this is the const version of find
    const Tree* find(const T& data) const;

    //return the depth of the node that contains the specified data in this whole tree
    //return -1 if the tree is empty or the data is not found
    //note: root node has a depth of 0
    int getDepth(const T& data) const;

    //return the descendant count of the node that contains the specified data in this whole tree
    //return -1 if the tree is empty or the data is not found
    //note: any direct or indirect child of a node is considered as a descendant of a node
    //see sample output for examples
    //hint: you may make use of the overloaded version of this function that takes no parameter
    int getDescendantCount(const T& data) const;

    //return the descendant count of the root node in this whole tree
    //return -1 if the tree is empty or the data is not found
    int getDescendantCount() const;

    //add a new root with the specified data to the tree, and return true at the end
    //if the tree already has a root (i.e. non-empty tree), then make a new root while keeping the existing tree
    //that means, you will create a new root node with the specified data; it also has exactly one child which is the old root
    //note: if the specified data already exists in the tree, do nothing and return false
    bool addRoot(const T& data); 

    //add a new node (child with data being childData) to an existing node (parent with data being parentData) in the tree, and return true at the end
    //however, if the parent node is not found in the tree or if the child data is already in the tree, do nothing and return false
    //you should add the child to the parent node's children array so that the children[0] is the first child ever added, children[1] is the second one, and so on
    //this is to make sure your output is always the same as ours
    bool addChild(const T& parentData, const T& childData);

    //print the tree to the ostream
    //given; just like the rest of this file, do not modify
    void print(std::ostream &os, bool atRoot = true, std::string indentation = "", bool lastChild = true) const
    {
        os << indentation;
        if (!atRoot) //add indentation for non-root nodes
        {
            if (lastChild)
            {
                os << "\\-";
                indentation += "  ";
            }
            else
            {
                os << "|-";
                indentation += "| ";
            }
        }
        os << root->data << std::endl;

        for (int i = 0; i < root->childCount; i++)
            root->children[i].print(os, false, indentation, i == root->childCount - 1);
    }

private:
    Node<T>* root = nullptr; //root node of the tree
};

/* 
 * The following statement effectively copies and pastes the content of the
 * treeImplementation.h here because the template member function definitions
 * need to be here after the class template declaration
 * This is a trick to allow students to write code in a separate file
 */
#include "treeImplementation.h"

#endif