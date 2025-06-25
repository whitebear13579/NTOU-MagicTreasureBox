#include<iostream>
using namespace std;

int f(int x);

int main() {

    cout << f(6) << "\n";
    cout << f(7) << "\n";
}

int f(int x) {
    int result = x + 1;
    return result;
}
