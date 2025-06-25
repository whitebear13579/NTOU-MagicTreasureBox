#include<iostream>
using namespace std;

int main() {

    int i = 5, j = 5, k = 3, l = 7;

    cout << boolalpha;
    cout << (i != j && (k += 2) < l) << "\n";
    cout << k << "\n";

    return 0;
}

