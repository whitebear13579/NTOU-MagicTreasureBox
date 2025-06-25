#include<iostream>
using namespace std;

int gcd(int p, int q) {
    if (q == 0) return p;
    return gcd(q, p % q);
}

int main() {

    cout << gcd(24, 12) << "\n";
    cout << gcd(30, 25) << "\n";
    cout << gcd(102, 68) << "\n";
}






