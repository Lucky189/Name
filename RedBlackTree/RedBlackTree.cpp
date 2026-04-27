#include <iostream>
#include "RedBlackTree.h"

int main()
{
    RedBlackTree<int> tree;

    tree.insert(10);
    tree.insert(20);
    tree.insert(5);
    tree.insert(15);

    std::cout << "Inorder: ";
    tree.inorder();

    std::cout << "Search 15: " << tree.search(15) << "\n";
    std::cout << "Search 100: " << tree.search(100) << "\n";

    return 0;
}