#pragma once
#include <iostream>

template <typename T>
class RedBlackTree
{
private:
    enum Color { RED, BLACK };

    struct Node
    {
        T data;
        Color color;
        Node* left;
        Node* right;
        Node* parent;

        Node(const T& value, Color color, Node* nil)
            : data(value), color(color), left(nil), right(nil), parent(nil) {
        }
    };

    Node* root;
    Node* NIL; 

private:


    void leftRotate(Node* x)
    {
        Node* y = x->right;
        x->right = y->left;

        if (y->left != NIL)
            y->left->parent = x;

        y->parent = x->parent;

        if (x->parent == NIL)
            root = y;
        else if (x == x->parent->left)
            x->parent->left = y;
        else
            x->parent->right = y;

        y->left = x;
        x->parent = y;
    }

    void rightRotate(Node* y)
    {
        Node* x = y->left;
        y->left = x->right;

        if (x->right != NIL)
            x->right->parent = y;

        x->parent = y->parent;

        if (y->parent == NIL)
            root = x;
        else if (y == y->parent->right)
            y->parent->right = x;
        else
            y->parent->left = x;

        x->right = y;
        y->parent = x;
    }

    void insertFix(Node* z)
    {
        while (z->parent->color == RED)
        {
            if (z->parent == z->parent->parent->left)
            {
                Node* y = z->parent->parent->right; 

                if (y->color == RED)
                {
                    z->parent->color = BLACK;
                    y->color = BLACK;
                    z->parent->parent->color = RED;
                    z = z->parent->parent;
                }
                else
                {
                    if (z == z->parent->right)
                    {
                        z = z->parent;
                        leftRotate(z);
                    }

                    z->parent->color = BLACK;
                    z->parent->parent->color = RED;
                    rightRotate(z->parent->parent);
                }
            }
            else
            {
                Node* y = z->parent->parent->left;

                if (y->color == RED)
                {
                    z->parent->color = BLACK;
                    y->color = BLACK;
                    z->parent->parent->color = RED;
                    z = z->parent->parent;
                }
                else
                {
                    if (z == z->parent->left)
                    {
                        z = z->parent;
                        rightRotate(z);
                    }

                    z->parent->color = BLACK;
                    z->parent->parent->color = RED;
                    leftRotate(z->parent->parent);
                }
            }
        }

        root->color = BLACK;
    }

    void destroy(Node* node)
    {
        if (node == NIL) return;

        destroy(node->left);
        destroy(node->right);
        delete node;
    }

    void inorder(Node* node) const
    {
        if (node == NIL) return;
        inorder(node->left);
        std::cout << node->data << " ";
        inorder(node->right);
    }

    void preorder(Node* node) const
    {
        if (node == NIL) return;
        std::cout << node->data << " ";
        preorder(node->left);
        preorder(node->right);
    }

    void postorder(Node* node) const
    {
        if (node == NIL) return;
        postorder(node->left);
        postorder(node->right);
        std::cout << node->data << " ";
    }

public:


    RedBlackTree()
    {
        NIL = new Node(T{}, BLACK, nullptr);
        NIL->left = NIL->right = NIL->parent = NIL;
        root = NIL;
    }


    ~RedBlackTree()
    {
        destroy(root);
        delete NIL;
    }


    RedBlackTree(const RedBlackTree&) = delete;
    RedBlackTree& operator=(const RedBlackTree&) = delete;


    RedBlackTree(RedBlackTree&& other) noexcept
    {
        root = other.root;
        NIL = other.NIL;

        other.root = nullptr;
        other.NIL = nullptr;
    }

    RedBlackTree& operator=(RedBlackTree&& other) noexcept
    {
        if (this != &other)
        {
            destroy(root);
            delete NIL;

            root = other.root;
            NIL = other.NIL;

            other.root = nullptr;
            other.NIL = nullptr;
        }
        return *this;
    }


    bool isEmpty() const
    {
        return root == NIL;
    }

    bool search(const T& value) const
    {
        Node* current = root;

        while (current != NIL)
        {
            if (value == current->data)
                return true;
            else if (value < current->data)
                current = current->left;
            else
                current = current->right;
        }
        return false;
    }

    void insert(const T& value)
    {
        Node* z = new Node(value, RED, NIL);
        Node* y = NIL;
        Node* x = root;

        while (x != NIL)
        {
            y = x;
            if (z->data == x->data)
            {
                delete z; 
                return;
            }
            else if (z->data < x->data)
                x = x->left;
            else
                x = x->right;
        }

        z->parent = y;

        if (y == NIL)
            root = z;
        else if (z->data < y->data)
            y->left = z;
        else
            y->right = z;

        insertFix(z);
    }


    void inorder() const
    {
        inorder(root);
        std::cout << "\n";
    }

    void preorder() const
    {
        preorder(root);
        std::cout << "\n";
    }

    void postorder() const
    {
        postorder(root);
        std::cout << "\n";
    }
};
