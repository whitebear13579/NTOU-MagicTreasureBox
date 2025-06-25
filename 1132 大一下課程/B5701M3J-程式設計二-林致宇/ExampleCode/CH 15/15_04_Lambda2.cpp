#include<iostream>
using namespace std;

int sum(int a, int b) { return a+b; }
// int product(int a, int b) { return a*b; }

int calculate(int a, int b, int (*pfunc)(int a, int b)) {
    return pfunc(a, b);
}

int main() {
    cout << calculate(3, 5, sum) << endl;
    auto lambda1 {[](int a, int b){ return a*b; }};
    cout << calculate(3, 5, lambda1) << endl;
}







