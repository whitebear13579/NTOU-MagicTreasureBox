#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    int i = 5;
    double d = 3.0;

    int * pi = &i;
    double * pd = &d;

    cout << pi << ", " << *pi << ", " << sizeof(pi) << '\n';
    cout << pd << ", " << *pd << ", " << sizeof(pd) << '\n';
    cout << d << ", " << sizeof(d) << '\n';

    cout << &pi << ", " << &pd << '\n';
}


