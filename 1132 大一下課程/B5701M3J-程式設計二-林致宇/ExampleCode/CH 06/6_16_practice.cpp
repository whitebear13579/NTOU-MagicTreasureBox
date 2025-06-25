#include<iostream>
using namespace std;

void i(int x, int t) {

    if (x > t) {
        cout << "\n";
        return;
    }
    cout << x;
    i(x+1, t);
}

void o(int x, int t) {
    if (x == 0) {
        return;
    }
    i(1, t-x+1);
    o(x-1, t);
}

void f(int x) {
    o(x, x);
}

int main() {
    f(6);
}




