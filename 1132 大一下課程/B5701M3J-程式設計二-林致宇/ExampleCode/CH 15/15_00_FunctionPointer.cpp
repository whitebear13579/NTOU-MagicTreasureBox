#include<iostream>
using namespace std;

int sum(int a, int b) { return a+b; }
int product(int a, int b) { return a*b; }

int main() {
    int (*pfunc)(int a, int b);

    pfunc = sum;
    cout << pfunc(3, 5) << endl;

    pfunc = product;
    cout << pfunc(3, 5) << endl;
}





