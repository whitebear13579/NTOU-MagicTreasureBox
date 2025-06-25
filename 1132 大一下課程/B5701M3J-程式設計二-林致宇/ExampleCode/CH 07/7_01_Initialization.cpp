#include<iostream>
using namespace std;

int main() {
    int iarray1[3] = {1, 6, 8};
    int iarray2[] = {2, 5};

    for (int i = 0; i < 3; i++) {
        cout << iarray1[i] << "\t";
    }
    cout << "\n";
    for (int i = 0; i < 2; i++) {
        cout << iarray2[i] << "\t";
    }
    cout << "\n";
}
