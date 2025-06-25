#include<iostream>
using namespace std;

int main() {

    int iarray[3];
    iarray[0] = 1;
    iarray[1] = 6;
    iarray[2] = 8;

    cout << sizeof(iarray) << "\n";
    for (int i = 0; i < 3; i++) {
        cout << iarray[i] << "\t";
    }
    cout << "\n";
}
