#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<math.h>

unsigned long long my_rand();
void my_seed(unsigned long long s) ;

struct node {
    int data;
    struct node *left, *right;
};

struct node* get_node()
{
    struct node *t = (struct node*) malloc(sizeof(struct node));
    t->left = NULL;
    t->right= NULL;
    return t;
}
struct node* search_binary_search_tree(struct node *root,int key)
{
    struct node *parent;
    struct node *p = root;
    while (p != NULL) {
        if (p->data == key) {
            return p;
        }
        parent = p;
        if (key < p->data) {
            p = p->left;
        } else {
            p = p->right;
        }
    }
    return parent;
}

void insert_into_binary_search_tree(struct node** root,int data) {
    if (*root == NULL) {
        *root = get_node();
        (*root)->data = data;
        return;
    } else {
        struct node *p = search_binary_search_tree(*root,data);
        if (p->data == data) {
            // data is already in the binary search tree
            return;
        } else {
            // insert data into the binary search tree
            struct node *t = get_node();
            t->data = data;
            if (data < p->data) {
                p->left = t;
            } else {
                p->right = t;
            }
        }
    }
    return;
}

void inorder(struct node *root)
{
    if (root != NULL) {
        inorder(root->left);
        //printf("%d\n",root->data);
        inorder(root->right);
    }
    return;
}

void preorder(struct node *root)
{
    if (root != NULL) {
        //printf("%d\n",root->data);
        preorder(root->left);
        preorder(root->right);
    }
    return;
}

void postorder(struct node *root)
{
    if (root != NULL) {
        postorder(root->left);
        postorder(root->right);
        //printf("%d\n",root->data);
    }
    return;
}

int depth(struct node *root) {
    if (root == NULL) {
        return 0; 
    }
    if (root->left == NULL && root->right == NULL) {
        // Case 4: 若root沒有左右子樹
        return 0;
    } else if (root->left != NULL && root->right == NULL) {
        // Case 2: 若root只有左子樹
        return depth(root->left) + 1;
    } else if (root->left == NULL && root->right != NULL) {
        // Case 3: 若root只有右子樹
        return depth(root->right) + 1;
    } else {
        return fmax(depth(root->left),depth(root->right))+1;
    }
}

int main()
{
    struct node *root = NULL;
    int i;

    my_seed(39393939);

    // insert 10000 numbers into an empty binary search tree.    
    for(i = 0; i < 10000; ++i) {
        insert_into_binary_search_tree(&root,my_rand()%20000);
    }

    // traverse the binary search tree in in-order.
    //printf("------------------------- inorder\n");
    inorder(root);

    // traverse the binary search tree in pre-order.
    //printf("------------------------- preorder\n");
    preorder(root);

    // traverse the binary search tree in post-order.
    //printf("------------------------- postorder\n");
    postorder(root);

    // Print the depth of the BST
    printf("Depth: %d\n", depth(root));

    return 0;
}

unsigned long long _u = 9837266468374616373ULL, _v = 4101842887655102021ULL, _w = 1;

unsigned long long my_rand()
{
    unsigned long long  x;
    _u  = _u * 2862933555777941757ULL + 7046029254386353087ULL;
    _v ^= _v >> 17; _v ^= _v << 31; _v ^= _v >> 8;
    _w  = 4294957665U * (_w & 0xffffffff) + (_w >> 32);
    x   = _u ^ (_u << 21); x ^= x >> 35; x ^= x << 4;
    return (x + _v) ^ _w;
}

void my_seed(unsigned long long s)
{
     unsigned long long t = _v;
    _w = _v;
    _v = s ^ _v;
    _u = t;
    return;
}