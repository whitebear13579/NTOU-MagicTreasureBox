#include<iostream>
#include<array>
using namespace std;

int main(int argc, const char * argv[]) {

    int iarray1[3] {1, 2, 3};
    int iarray2[3] {1, 2, 3};
    int iarray3[3] {1, 5, 3};

    if (iarray1 == iarray2) {
        cout << "Equal!" << endl;
    } else {
        cout << "Not Equal!" << endl;
    }
}



