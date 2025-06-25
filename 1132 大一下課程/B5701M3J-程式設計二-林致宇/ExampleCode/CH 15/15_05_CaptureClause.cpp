#include<iostream>
using namespace std;

int main() {
    int op = 1; // 1: sum, 2: product
    auto lambda1 {[=](int a, int b) {
        if (op == 1) {
            return a+b;
        } else {
            return a*b;
        }
    }};
    cout << lambda1(3,5) << endl;
    op = 2;
    cout << lambda1(3,5) << endl;
}



