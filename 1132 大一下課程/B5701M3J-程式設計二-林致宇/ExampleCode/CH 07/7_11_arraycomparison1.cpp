#include<iostream>
#include<array>
using namespace std;

int main(int argc, const char * argv[]) {

    array<int, 3> iarray1 {1, 2, 3};
    array<int, 3> iarray2 {1, 2, 3};
    array<int, 3> iarray3 {1, 5, 3};

    if (iarray1 == iarray2) {
        cout << "Equal!" << endl;
    } else {
        cout << "Not Equal!" << endl;
    }
}



