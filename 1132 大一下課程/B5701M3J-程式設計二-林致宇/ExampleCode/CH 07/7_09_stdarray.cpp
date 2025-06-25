#include<iostream>
#include<array>
using namespace std;

int main(int argc, const char * argv[]) {

    std:array<int, 10> iarray {};

    iarray.fill(8);

    for (int x: iarray) {
        cout << x << endl;
    }    
}


