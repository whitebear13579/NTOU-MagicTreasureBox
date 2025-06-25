#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    int i {8}, j{};
    int * const pi {&i};

    i = 6;
    cout << *pi << endl;

    // pi = &j; // error
}





