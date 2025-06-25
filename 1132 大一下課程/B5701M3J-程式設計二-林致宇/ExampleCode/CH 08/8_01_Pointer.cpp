#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    int x = 5;
    
    int * px = &x;

    cout << px << ", " << &px << '\n';
    cout << *px << ", " << *(&px) << '\n';

}


