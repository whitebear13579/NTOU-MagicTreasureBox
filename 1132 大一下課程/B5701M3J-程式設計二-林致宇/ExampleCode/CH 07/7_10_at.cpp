#include<iostream>
#include<array>
using namespace std;

int main(int argc, const char * argv[]) {

    array<int, 10> iarray1 {};
    int iarray2[10];

    iarray1.at(-1);
    iarray2[-1];
}


