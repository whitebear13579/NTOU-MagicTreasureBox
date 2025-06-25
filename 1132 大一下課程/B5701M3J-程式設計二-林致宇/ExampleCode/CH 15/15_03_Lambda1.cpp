#include<iostream>
using namespace std;

int sum(int a, int b) { return a+b; }
// int product(int a, int b) { return a*b; }

int calculate(int a, int b, int (*pfunc)(int a, int b)) {
    return pfunc(a, b);
}

int main() {
    cout << calculate(3, 5, sum) << endl;
    cout << calculate(3, 5, [](int a, int b){ return a*b; }) << endl;
}







