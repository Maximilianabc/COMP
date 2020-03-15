#include <iostream>     /* File: lab10.cpp */
#include <sstream> 
using namespace std;

/* The data type is changed to string */
struct btree_node       // A node in a binary tree
{
    string data;        // To store the recursive call content
    btree_node* left;   // Left sub-tree or called left child
    btree_node* right;  // Right sub-tree or called right child
};

/* The data type is changed to string */
btree_node* create_btree_node(string data)
{
    btree_node* node = new btree_node;
    node->data = data;
    node->left = node->right = nullptr;
    return node;
}

/* This function is exactly the same as in the lecture notes */
void delete_btree(btree_node*& root) // By recursion
{
    if (root == nullptr) return;     // Base case

    delete_btree(root->left);        // Recursion on the left subtree
    delete_btree(root->right);       // Recursion on the right subtree
    delete root;
    root = nullptr;
}

/* This function is exactly the same as in the lecture notes */
void print_btree(const btree_node* root, int depth = 0)
{
    if (root == nullptr)   // Base case
        return;

    print_btree(root->right, depth + 1);// Recursion: right subtree

    for (int j = 0; j < depth; j++)   // Print the node data
        cout << '\t';
    cout << root->data << endl;

    print_btree(root->left, depth + 1); // Recursion: left subtree
}


/* TODO: Rewrite the recursive fibonacci function so that it will also build
 * the recursive function call binary tree at the same time.
 * In our solution, there will be another formal parameter used to build the
 * tree.
 */
int fibonacci(int n, btree_node*& tree_node)
{
    tree_node = create_btree_node("f(" + to_string(n) + ")");
    if (n < 0) return -1;
    if (n == 0) return 0;
    if (n == 1) return 1;
    return fibonacci(n - 1, tree_node->left) + fibonacci(n - 2, tree_node->right);
}


/* TODO:
 * Sample driver program
 */
int main()
{
    int n;
    btree_node* root = nullptr;        // Root of the call tree to be constructed

    cout << "\n\nEnter an integer to compute its Fibonacci number (-ve number to stop): ";
    cin >> n;

    while (n >= 0)
    {
        cout << "Fibonacci(" + to_string(5) + ") = " << fibonacci(n, root) << endl;
        print_btree(root);
        delete_btree(root);
        cout << "\n\nEnter an integer to compute its Fibonacci number (-ve number to stop): ";
        cin >> n;
    }

    return 0;
}