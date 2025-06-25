#include<iostream>
using namespace std;

void i(int x) {
    if (x == 0) {
        cout << "\n";
        return;
    }
    cout << "*";
    i(x-1);
}

void o(int x) {
    if (x == 0) {
        return;
    }
    i(x);
    o(x-1);
}

int main() {
    o(5);
}




