#include<iostream>
#include <list>
using namespace std;

list<int>::iterator findMin(list<int> & fl, list<int>::iterator from) {
    
    list<int>::iterator checkedItem = from;
    list<int>::iterator min = from;
    checkedItem++;
    do {
        if (*checkedItem < *min) {
            min = checkedItem;
            
        }
        checkedItem++;
    } while (checkedItem != fl.end());
    return min;
}

void sortDLL(list<int> & fl) {

    list<int>::iterator wall = fl.begin();
    list<int>::iterator min;

    do {
        wall++;
        if (wall == fl.end()) {
            break;
        }
        wall--;
        min = findMin(fl, wall);
        if (min == wall) {
            wall++;
            continue;
        }

        fl.insert(wall, *min);
        fl.erase(min);
    } while (wall != fl.end());
}

void printDLLNumber(list<int> & fl) {

    for (list<int>::iterator p = fl.begin(); p != fl.end(); p++) {
        if (p != fl.begin()) {
            cout << ' ';
        }
        cout << *p;
    }
    cout << endl;
}

int main() {
    list<int> fl {32, 68, 57, 39, 86};

    printDLLNumber(fl);
    sortDLL(fl);
    printDLLNumber(fl);
}








