#include<iostream>
using namespace std;

struct number {
    int number;
    struct number * next;
};

struct number * findTarget(struct number * start, int target) {
    struct number * current = start;
    do {
        if (current->number == target) {
            return current;
        }
        current = current->next;
    } while (current != NULL);
    return NULL;
}

struct number * findTargetPrevious(struct number * start, int target) {
    struct number * current = start;
    do {
        if (current->next != NULL) {
            if (current->next->number == target) {
                return current;
            }
        }
        current = current->next;
    } while (current != NULL);
    return NULL;
}

void insertAfterTarget(struct number * start, int target, struct number * ins) {
    struct number * t = findTarget(start, target);
    struct number * tmp = t->next;
    ins->next = tmp;
    t->next = ins;
}

void deleteTarget(struct number * start, int target) {
    struct number * preOne = findTargetPrevious(start, target);
    struct number * t = preOne->next;

    preOne->next = t->next;
    delete t;
}

void printAll(struct number * start) {
    struct number * current = start;
    do {
        cout << current->number << endl;
        current = current->next;
    } while (current != NULL);
}

int main(int argc, char * argv[]) {
    struct number * start = new struct number;
    start->number = 3;
    start->next = NULL;

    struct number * x = new struct number;
    x->number = 7;
    insertAfterTarget(start, 3, x);
    struct number * y = new struct number;
    y->number = 5;
    insertAfterTarget(start, 3, y);
    // printAll(start);

    deleteTarget(start, 5);
    printAll(start);
}


