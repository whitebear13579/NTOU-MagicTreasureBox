#include<iostream>
#include <forward_list>
using namespace std;

int main() {
    forward_list<int> fl {32, 68, 57, 39, 86};

    forward_list<int>::iterator p;

    for (p = fl.begin(); p != fl.end(); p++) {
        cout << *p << endl;
    }
}






