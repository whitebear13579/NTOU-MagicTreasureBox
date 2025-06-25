#include<iostream>
#include <vector>
using namespace std;

int main() {
    vector<int> v {32, 68, 57, 39, 86};

    vector<int>::iterator p;

    for (p = v.begin(); p != v.end(); p++) {
        cout << *p << endl;
    }

    vector<int>::reverse_iterator rp;

    for (rp = v.rbegin(); rp != v.rend(); rp++) {
        cout << *rp << endl;
    }
}






