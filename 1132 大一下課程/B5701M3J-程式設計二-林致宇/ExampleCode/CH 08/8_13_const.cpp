#include<iostream>
#include<vector>
using namespace std;

int main(int argc, char * argv[]) {
    
    const int i {8};
    int j {};
    // int *pi {&i}; // error
    const int *pi {&i};

    pi = &j;
}



